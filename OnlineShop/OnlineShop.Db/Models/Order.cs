using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserDeliveryInfo UserDeliveryInfo { get; set; }
        public List<BasketItem> Items { get; set; }
        public OrderStatuses Status { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Order()
        {
            Status = OrderStatuses.Created;
            CreateDateTime = DateTime.Now;
        }
    }
}
