using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db.Storages
{
    public class FavoritesDbStorage : IFavoritesStorage
    {
        private readonly DatabaseContext _databaseContext;

        public FavoritesDbStorage(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AppendAsync(string userId, Product product)
        {
            var existingProduct = await _databaseContext.Favorites.FirstOrDefaultAsync(x => x.UserId == userId && x.Product.Id == product.Id);
            if(existingProduct == null)
            {
                _databaseContext.Favorites.Add(new FavoriteProduct { Product = product, UserId = userId });
                await _databaseContext.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAllAsync(string userId)
        {
            return await _databaseContext.Favorites.Where(x => x.UserId == userId)
                                             .Include(x => x.Product)
                                             .ThenInclude(x => x.Images)
                                             .Select(x => x.Product)
                                             .ToListAsync();
        }

        public async Task RemoveAsync(string userId, Guid productId)
        {
            var removingFavorite = await _databaseContext.Favorites.FirstOrDefaultAsync(x => x.UserId == userId && x.Product.Id == productId);
            _databaseContext.Favorites.Remove(removingFavorite);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
