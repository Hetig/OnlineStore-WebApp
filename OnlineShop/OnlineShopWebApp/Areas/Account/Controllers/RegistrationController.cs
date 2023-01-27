using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Account.Models;
using Serilog;
using System;

namespace OnlineShopWebApp.Areas.Account.Controllers
{
    [Area("Account")]
	public class RegistrationController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public RegistrationController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Index(string returnUrl)
		{
			return View(new Registration() { ReturnUrl = returnUrl });
		}

		[HttpPost]
		public IActionResult Register(Registration registration)
		{
			if (registration.Login.Equals(registration.Password))
			{
				ModelState.AddModelError("", "Логин и пароль не должны совпадать");
			}

			if (ModelState.IsValid)
			{
				var user = new User 
				{ 
					Email = registration.Login, 
					UserName = registration.Login,
				};

				var result = _userManager.CreateAsync(user, registration.Password).Result;
				if (result.Succeeded)
				{
					_signInManager.SignInAsync(user, isPersistent: false).Wait();

					TryAssignUserRole(user);

					return Redirect(registration.ReturnUrl ?? "/Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}

			return View("Index", registration);
		}

		private void TryAssignUserRole(User user)
		{
			try
			{
				_userManager.AddToRoleAsync(user, Constans.UserRoleName).Wait();
			}
			catch
			{
			}
		}
	}
}
