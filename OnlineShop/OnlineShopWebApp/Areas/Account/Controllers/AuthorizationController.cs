using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Account.Models;

namespace OnlineShopWebApp.Areas.Account.Controllers
{
    [Area("Account")]
	public class AuthorizationController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}


		public IActionResult Index(string returnUrl)
		{
			return View(new Authorization() { ReturnUrl = returnUrl });
		}


		[HttpPost]
		public IActionResult Authorize(Authorization authorization)
		{
			if (ModelState.IsValid)
			{
				var result = _signInManager.PasswordSignInAsync(authorization.Login, authorization.Password, authorization.RememberMe, false).Result;
				if (result.Succeeded)
				{
					return Redirect(authorization.ReturnUrl ?? "/Home");
				}
				else
				{
					ModelState.AddModelError("", "Неправильный логин или пароль");
				}
			}

			return View("Index", authorization);
		}

		public IActionResult Exit()
		{
			_signInManager.SignOutAsync().Wait();

			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}
