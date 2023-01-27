using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.VeiwModels;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly IProductsStorage _productsStorage;
        private readonly IBasketStorage _basketsStorage;
		private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
		private readonly string userId;

		public BasketController(IProductsStorage productsStorage, IBasketStorage basketsStorage, IHttpContextAccessor httpContextAccessor, IMapper mapper)
		{
			_productsStorage = productsStorage;
			_basketsStorage = basketsStorage;
			_httpContextAccessor = httpContextAccessor;
			_mapper = mapper;

			userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId is null)
			{
				var cookies = _httpContextAccessor.HttpContext.Request.Cookies;
				if (cookies.ContainsKey("CurrentUserId"))
				{
					userId = cookies["CurrentUserId"];
				}
				else
				{
					userId = Guid.NewGuid().ToString();
					_httpContextAccessor.HttpContext.Response.Cookies.Append("CurrentUserId", userId);
				}
			}
		}

		public async Task<IActionResult> AddAsync(Guid productId)
        {
            var productDb = await _productsStorage.TryGetByIdAsync(productId);
            await _basketsStorage.AddAsync(productDb, userId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var basketDb = await _basketsStorage.TryGetByUserIdAsync(userId);
            var model = _mapper.Map<BasketViewModel>(basketDb);
            return View(model);
        }

        public async Task<IActionResult> ClearAsync()
        {
            await _basketsStorage.ClearAsync(userId);
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangeAmountAsync(Guid itemId, bool flag)
        {               
            await _basketsStorage.ChangeItemAmountAsync(userId, itemId, flag);
            return RedirectToAction("Index");
        }       
    }
}
