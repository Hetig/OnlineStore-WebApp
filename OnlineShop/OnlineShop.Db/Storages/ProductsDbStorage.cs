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
		/// <summary>
		/// Класс ProductsDbStorage является хранилищем для продуктов,
		/// которые есть на сайте. С помощью этого класса происходит взаимодействие
		/// с товарами. Класс реализует интерфейс IProductsStorage, который находится в контейнере зависимостей DI,
		/// благодаря чему взаимодействие с хранилищем товаров становится очень удобным. На мой взгляд в классе нет ничего лишнего,
		/// все аккуратно и по существу.
		/// </summary>
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

		/// <summary>
		/// Метод ChangeFavoriteStatusAsync меняет булевое состояние продукта, которое отображает
		/// находится ли продукт в Корзине Избранных товаров
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Метод ChangeProductAsync меняет значения членов продукта, при его редактировании.
		/// Редактирование продукта доступно только пользователям имеющим роль Администратора сайта.
		/// </summary>
		/// <param name="newProduct"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Метод FindProductsAsync реализует поиск товаров по введенным в поисковик значениям.
		/// Поиск производится по словам, которые были введены в инпут. Если хоть одно слово совпало
		/// с названием продукта, то оно будет выведено в результате. Из результата поиска выводятся все товары без повторений.
		/// </summary>
		/// <param name="productName"></param>
		/// <returns></returns>
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
