using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Views.Shared.Components.AvatarImage
{
	public class AvatarImageViewComponent : ViewComponent
	{
		private readonly UserManager<User> _userManager;

		public AvatarImageViewComponent(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public IViewComponentResult Invoke()
		{
			var user = _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User).Result;
			return View("AvatarImage", user.AvatarImageUrl);
		}
	}
}
