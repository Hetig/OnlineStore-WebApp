using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsStorage
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> TryGetByIdAsync(Guid id);
		Task AppendAsync(Product product);
		Task ChangeFavoriteStatusAsync(Guid productId);
		Task RemoveByIdAsync(Guid productId);
		Task ChangeProductAsync(Product newProduct);
		Task<List<Product>> FindProductsAsync(string productName);
    }
}
