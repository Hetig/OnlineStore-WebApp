using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrdersStorage
    {
        Task AppendAsync(Order order);

        Task<List<Order>> GetAllAsync();

		Task<Order> TryGetByIdAsync(Guid id);

		Task ChangeOrderStatusAsync(OrderStatuses status, Guid orderId);
		Task<List<Order>> TryGetByUserIdAsync(Guid userId);

	}
}