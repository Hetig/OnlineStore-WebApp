using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Helpers
{
    public class ProductComparator : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Product obj)
        {
            return HashCode.Combine(obj.Id);
        }
    }
}
