using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Interfaces
{
    public interface IFavoritesStorage
    {
        Task<List<Product>> GetAllAsync(string userId);

        Task AppendAsync(string userId, Product product);

        Task RemoveAsync(string userId, Guid productId);
    }
}