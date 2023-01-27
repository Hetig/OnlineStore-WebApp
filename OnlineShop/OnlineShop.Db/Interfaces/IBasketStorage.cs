using OnlineShop.Db.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IBasketStorage
    {
        Task AddAsync(Product product, string userId);
        Task<Basket> TryGetByUserIdAsync(string userId);
		Task ChangeItemAmountAsync(string userId, Guid itemId, bool flag);
		Task ClearAsync(string userId);
    }
}
