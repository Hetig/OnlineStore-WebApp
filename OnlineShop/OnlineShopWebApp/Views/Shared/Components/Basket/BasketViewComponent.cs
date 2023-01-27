using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.VeiwModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Views.Components.Basket
{
	public class BasketViewComponent : ViewComponent
    {
        private readonly IBasketStorage _basketsStorage;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IMapper _mapper;
		private readonly string userId;


		public BasketViewComponent(IBasketStorage basketsStorage, IHttpContextAccessor httpContextAccessor, IMapper mapper)
		{
			_basketsStorage = basketsStorage;
			_httpContextAccessor = httpContextAccessor;
			_mapper = mapper;

			userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userId))
			{
				userId = _httpContextAccessor.HttpContext.Request.Cookies["CurrentUserId"];
			}
		}

		public async Task<IViewComponentResult> InvokeAsync()
        {
			var basketDb = await _basketsStorage.TryGetByUserIdAsync(userId);
            var basketViewModel = _mapper.Map<BasketViewModel>(basketDb);
           
            var productsCount = basketViewModel?.Amount ?? 0;

            return View("Basket", productsCount);
        }
    }
}
