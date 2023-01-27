using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db.Storages
{
    public class BasketsDbStorage : IBasketStorage
    {
        private readonly DatabaseContext _databaseContext;

        public BasketsDbStorage(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Basket> TryGetByUserIdAsync(string userId)
        {
            return  await _databaseContext.Baskets.Include(x => x.Items).ThenInclude(x => x.Product).ThenInclude(x => x.Images).FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task AddAsync(Product product, string userId)
        {
            var existingBasket = await TryGetByUserIdAsync(userId);

            if (existingBasket == null)
            {
                var newBasket = new Basket
                {
                    UserId = userId,
                };

                newBasket.Items = new List<BasketItem>
                {
                     new BasketItem
                     {
                          Amount = 1,
                          Product = product
                     }
                };

                _databaseContext.Baskets.Add(newBasket);
            }
            else
            {
                var existingBasketItem = existingBasket.Items.FirstOrDefault(x => x.Product.Id == product.Id);

                if (existingBasketItem != null)
                {
                    existingBasketItem.Amount += 1;
                }
                else
                {
                    existingBasket.Items.Add(new BasketItem
                    {
                        Amount = 1,
                        Product = product                       
                    });
                }
            }

			await _databaseContext.SaveChangesAsync();
		}

		public async Task ChangeItemAmountAsync(string userId, Guid itemId, bool flag)
        {
            var basket = await TryGetByUserIdAsync(userId);
            var item = basket.Items.FirstOrDefault(x => x.Id == itemId);

            if (flag == true) item.Amount++;
            else
            {
                item.Amount--;
                if (item.Amount == 0)
                {
                    basket.Items.Remove(item);
                    _databaseContext.Items.Remove(item);
                }
            }

            await _databaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var basket = await TryGetByUserIdAsync(userId);
            _databaseContext.Baskets.Remove(basket);

            await _databaseContext.SaveChangesAsync();
        }
    }
}
