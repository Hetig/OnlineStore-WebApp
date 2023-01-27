using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.VeiwModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IBasketStorage _basketsStorage;
		private readonly IOrdersStorage _ordersStorage;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IMapper _mapper;
		private readonly string userId;

		public OrderController(IBasketStorage basketsStorage, IOrdersStorage odersStorage, IHttpContextAccessor httpContextAccessor, IMapper mapper)
		{
			_basketsStorage = basketsStorage;
			_ordersStorage = odersStorage;
			_httpContextAccessor = httpContextAccessor;
			_mapper = mapper;

			userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}

		public async Task<IActionResult> Index()
		{
			await CopyBasketItemsForAuthorizedUserAsync();

			return View();
		}

		private async Task CopyBasketItemsForAuthorizedUserAsync()
		{
			var cookies = _httpContextAccessor.HttpContext.Request.Cookies;

			string notAuthorizedUserId = "";
			if (cookies.ContainsKey("CurrentUserId"))
			{
				notAuthorizedUserId = cookies["CurrentUserId"];
			}

			if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(notAuthorizedUserId))
				return;

			var notAutorizedUserStorageAsync = await _basketsStorage.TryGetByUserIdAsync(notAuthorizedUserId);
			var notAutorizedUserStorage = notAutorizedUserStorageAsync?.Items;

			if (notAutorizedUserStorage != null && notAutorizedUserStorage.Any())
			{
				foreach (var item in notAutorizedUserStorage)
				{
					for (var i = 0; i < item.Amount; i++)
					{
						await _basketsStorage.AddAsync(item.Product, userId);
					}
				}

				await _basketsStorage.ClearAsync(notAuthorizedUserId);

				_httpContextAccessor.HttpContext.Response.Cookies.Delete("notAutorizedUserGuid");
			}
		}

		[HttpPost]
		public async Task<IActionResult> BuyAsync(UserDeliveryInfoViewModel userInfo)
		{
			if (ModelState.IsValid)
			{
				var cookies = _httpContextAccessor.HttpContext.Request.Cookies;
				var basketDbAsync = await _basketsStorage.TryGetByUserIdAsync(userId);
				var basketDbItems = basketDbAsync.Items;

				var orderDb = new Order
				{
					UserId = Guid.Parse(userId),
					UserDeliveryInfo = _mapper.Map<UserDeliveryInfo>(userInfo),
					Items = basketDbItems
				};


				await _ordersStorage.AppendAsync(orderDb);

				await _basketsStorage.ClearAsync(userId);

				return View("OrderHasBeenPaid");
			}

			return RedirectToAction("Index");
		}
	}
}
