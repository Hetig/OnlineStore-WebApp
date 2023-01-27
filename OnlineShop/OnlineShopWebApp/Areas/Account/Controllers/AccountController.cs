using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Account.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using OnlineShopWebApp.VeiwModels;

namespace OnlineShopWebApp.Areas.Account.Controllers
{
	[Area("Account")]
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly IOrdersStorage _ordersStorage;
		private readonly IImagesProvider _imageProvider;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IMapper _mapper;
		private readonly string currentUserId;

		public AccountController(UserManager<User> userManager, IImagesProvider imageProvider, IOrdersStorage ordersStorage, IHttpContextAccessor httpContextAccessor, IMapper mapper)
		{
			_userManager = userManager;
			_imageProvider = imageProvider;
			_ordersStorage = ordersStorage;
			_httpContextAccessor = httpContextAccessor;
			_mapper = mapper;

			currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}

		public IActionResult UserInfo()
		{
			var user = _userManager.GetUserAsync(User).Result;
			return View(user);
		}

		public async Task<IActionResult> Orders()
		{
			var orders = await _ordersStorage.TryGetByUserIdAsync(Guid.Parse(currentUserId));
			var models = _mapper.Map<List<OrderViewModel>>(orders);
			return View(models);
		}

		[HttpGet]
		public IActionResult EditUserData()
		{
			return View();
		}

		[HttpPost]
		public IActionResult EditUserData(EditUserData editUserData)
		{
			var user = _userManager.GetUserAsync(User).Result;

			user.FirstName = editUserData.FirstName ?? user.FirstName;
			user.LastName = editUserData.LastName ?? user.LastName;
			user.Address = editUserData.Address ?? user.Address ;
			user.PhoneNumber = editUserData.PhoneNumber ?? user.PhoneNumber;

			if(editUserData.UploadedFile != null)
			{
				var avatarPath = _imageProvider.SafeFile(editUserData.UploadedFile, ImageFolders.Profiles);
				user.AvatarImageUrl = avatarPath;
			}

			_userManager.UpdateAsync(user).Wait();

			return RedirectToAction("UserInfo");
		}
	}
}
