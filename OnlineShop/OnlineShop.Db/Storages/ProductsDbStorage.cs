using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Helpers;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db.Storages
{
	public class ProductsDbStorage : IProductsStorage
	{
		private readonly DatabaseContext _databaseContext;

		public ProductsDbStorage(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		public async Task<List<Product>> GetAllAsync()
		{
			return await _databaseContext.Products.Include(x => x.Images).ToListAsync();
		}

		public async Task<Product> TryGetByIdAsync(Guid id)
		{
			var productsList = await GetAllAsync();
			var product = productsList.FirstOrDefault(product => product.Id == id);
			return product;
		}

		public async Task AppendAsync(Product product)
		{
			await _databaseContext.Products.AddAsync(product);
			await _databaseContext.SaveChangesAsync();
		}

		public async Task ChangeFavoriteStatusAsync(Guid productId)
		{
			var product = await TryGetByIdAsync(productId);

			product.IsFavorite = !product.IsFavorite;

			await _databaseContext.SaveChangesAsync();
		}

		public async Task RemoveByIdAsync(Guid productId)
		{
			var product = _databaseContext.Products.Include(x => x.Images).Include(x => x.Items).FirstOrDefault(x => x.Id == productId);

			_databaseContext.Remove(product);

			await _databaseContext.SaveChangesAsync();
		}

		public async Task ChangeProductAsync(Product newProduct)
		{
			var existingProduct = await TryGetByIdAsync(newProduct.Id);
			if (existingProduct is null) return;

			existingProduct.Name = newProduct.Name;
			existingProduct.Description = newProduct.Description;
			existingProduct.Cost = newProduct.Cost;

			foreach (var image in newProduct.Images)
			{
				image.ProductId = existingProduct.Id;
				await _databaseContext.Images.AddAsync(image);
			}

			await _databaseContext.SaveChangesAsync();
		}

		public async Task<List<Product>> FindProductsAsync(string productName)
		{
			var products = await GetAllAsync();
			var searchWords = productName.ToLower().Split();
			var findResult = new List<IEnumerable<Product>>();

			foreach (var word in searchWords)
			{
				var foundProducts = products.Where(x => x.Name.ToLower().Split().Contains(word));
				findResult.Add(foundProducts);
			}

			var result = findResult.SelectMany(x => x).Distinct(new ProductComparator()).ToList();

			return result;
		}
	}
}
