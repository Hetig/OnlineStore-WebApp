using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public bool IsFavorite { get; set; }
        public List<BasketItem> Items { get; set; }
        public List<Image> Images { get; set; }

        public Product()
        {
            Items = new List<BasketItem>();
            Images = new List<Image>();
        }
    }
}
