using System;

namespace OnlineShopWebApp.VeiwModels
{
    public class BasketItemViewModel
    {
        public Guid Id { get; set; }
        public ProductViewModel Product { get; set; }
        public int Amount { get; set; }
        public decimal Cost
        {
            get
            {
                return Product.Cost * Amount;
            }
        }

        public BasketItemViewModel(Guid id, ProductViewModel product, int amount)
        {
            Id = id;
            Product = product;
            Amount = amount;
        }
    }
}
