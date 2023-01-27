using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Interfaces
{
    public interface IComparedProductsStorage
    {
        Task AppendAsync(string userId, Product product);
        Task RemoveAsync(string userId, Guid productId);
        Task<List<Product>> GetAllAsync(string userId);
    }
}