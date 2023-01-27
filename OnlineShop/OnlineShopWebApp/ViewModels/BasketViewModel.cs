using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.VeiwModels
{
    public class BasketViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }

        public List<BasketItemViewModel> Items = new List<BasketItemViewModel>();

        public BasketViewModel(Guid id, string userId, List<BasketItemViewModel> items)
        {
            Id = id;
            UserId = userId;
            Items = items;
        }

        public decimal Cost
        {
            get
            {
                return Items?.Sum(x => x.Cost) ?? 0;
            }
        }

        public int Amount
        {
            get
            {
                return Items?.Sum(x => x.Amount) ?? 0;
            }
        }
    }
}
