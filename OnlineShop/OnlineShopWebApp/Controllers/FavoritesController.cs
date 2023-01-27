using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.VeiwModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        public IFavoritesStorage _favoritesProducts;
        public IProductsStorage _products;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IMapper _mapper;
		private readonly string userId;

		public FavoritesController(IFavoritesStorage favoritesProducts, IProductsStorage products, IHttpContextAccessor httpContextAccessor, IMapper mapper)
		{
			_favoritesProducts = favoritesProducts;
			_products = products;
			_httpContextAccessor = httpContextAccessor;
			_mapper = mapper;

			userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
		public async Task<IActionResult> Index()
        {
            var productsDb = await _favoritesProducts.GetAllAsync(userId);
			var models = _mapper.Map<List<Product>, List<ProductViewModel>>(productsDb);

			return View(models);
        }
        public async Task<IActionResult> Add(Guid productId)
        {
            await _products.ChangeFavoriteStatusAsync(productId);

            var productDb = await _products.TryGetByIdAsync(productId);
            if (productDb != null)
            {
                await _favoritesProducts.AppendAsync(userId, productDb);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Remove(Guid productId)
        {
            await _favoritesProducts.RemoveAsync(userId, productId);
            await _products.ChangeFavoriteStatusAsync(productId);

            return RedirectToAction("Index");
        }
    }
}
