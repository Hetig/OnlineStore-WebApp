using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db.Storages
{
    public class OrdersDbStorage : IOrdersStorage
    {
        private readonly DatabaseContext _databaseContext;

        public OrdersDbStorage(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
		}

        public async Task AppendAsync(Order order)
        {
            _databaseContext.Orders.Add(order);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _databaseContext.Orders.Include(x => x.UserDeliveryInfo)
                                          .Include(x => x.Items)
                                          .ThenInclude(x => x.Product)
										  .ThenInclude(x => x.Images)
										  .Where(x => !x.Items.Select(x => x.Product).Contains(null))
										  .ToListAsync();
        }

        public async Task<Order> TryGetByIdAsync(Guid id)
        {
            var allOrders = await GetAllAsync();
			return allOrders.FirstOrDefault(x => x.Id == id);
		}

        public async Task<List<Order>> TryGetByUserIdAsync(Guid userId)
        {
            var userOrders = await GetAllAsync();          
			return userOrders.Where(x => x.UserId == userId).ToList();
		}

        public async Task ChangeOrderStatusAsync(OrderStatuses status, Guid orderId)
        {
            var order = await TryGetByIdAsync(orderId);
            order.Status = status;
            await _databaseContext.SaveChangesAsync();
        }
    }
}
