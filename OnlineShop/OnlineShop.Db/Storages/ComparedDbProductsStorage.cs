using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db.Storages
{
    public class ComparedDbProductsStorage : IComparedProductsStorage
    {
        private readonly DatabaseContext _databaseContext;

        public ComparedDbProductsStorage(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AppendAsync(string userId, Product product)
        {
            var existingProduct = await _databaseContext.ComparedProducts.FirstOrDefaultAsync(x => x.UserId == userId && x.Product.Id == product.Id);
            if (existingProduct == null)
            {
                await _databaseContext.ComparedProducts.AddAsync(new ComparedProduct { Product = product, UserId = userId });
                await _databaseContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(string userId, Guid productId)
        {
            var removingComparedProduct = await _databaseContext.ComparedProducts.FirstOrDefaultAsync(x => x.UserId == userId && x.Product.Id == productId);
            _databaseContext.ComparedProducts.Remove(removingComparedProduct);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync(string userId)
        {
            return await _databaseContext.ComparedProducts.Where(x => x.UserId == userId)
                                                    .Include(x => x.Product)
                                                    .ThenInclude(x => x.Images)
                                                    .Select(x => x.Product)
                                                    .ToListAsync();
        }
    }
}
