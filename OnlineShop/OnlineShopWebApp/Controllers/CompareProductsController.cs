using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
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
    public class CompareProductsController : Controller
    {
        private readonly IComparedProductsStorage _comparedProductsStorage;
        private readonly IProductsStorage _productsStorage;
		private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
		private readonly string userId;


		public CompareProductsController(IComparedProductsStorage comparedProductsStorage, IProductsStorage productsStorage, IHttpContextAccessor httpContextAccessor, IMapper mapper)
		{
			_comparedProductsStorage = comparedProductsStorage;
			_productsStorage = productsStorage;
			_httpContextAccessor = httpContextAccessor;
			_mapper = mapper;

			userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}

		public async Task<IActionResult> Index()
        {
            var productsDb = await _comparedProductsStorage.GetAllAsync(userId);
			var models = _mapper.Map<List<Product>, List<ProductViewModel>>(productsDb);
			return View(models);
        }

        public async Task<IActionResult> CompareAsync(Guid productId)
        {
            var productDb = await _productsStorage.TryGetByIdAsync(productId);

            if (productDb != null)
            {
                await _comparedProductsStorage.AppendAsync(userId, productDb);
            }
            return RedirectToAction("Index", "Favorites");
        }

        public async Task<IActionResult> RemoveAsync(Guid productId)
        {
            await _comparedProductsStorage.RemoveAsync(userId, productId);

            return RedirectToAction("Index");
        }
    }
}
