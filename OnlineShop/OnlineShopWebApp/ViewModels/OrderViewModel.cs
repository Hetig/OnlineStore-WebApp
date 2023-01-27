using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.VeiwModels
{
    public class OrderViewModel
    {	
		public Guid Id { get; set; }
        public UserDeliveryInfoViewModel UserDeliveryInfo { get; set; }
        public List<BasketItemViewModel> Items { get; set; }
        public OrderStatusesViewModel Status { get; set; }
        public DateTime CreateDateTime { get; set; }
		public decimal Cost
		{
			get
			{
				return Items?.Sum(x => x.Cost) ?? 0;
			}
		}
	}
}
