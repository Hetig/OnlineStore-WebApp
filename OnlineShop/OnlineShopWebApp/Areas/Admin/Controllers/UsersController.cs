using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.VeiwModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class UsersController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IMapper _mapper;


		public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			var users = _userManager.Users.ToList();
			var models = _mapper.Map<List<UserViewModel>>(users);
			return View(models);
		}

		public async Task<IActionResult> UserInfo(Guid userId)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			var model = _mapper.Map<UserViewModel>(user);
			return View(model);
		}

		public async Task<IActionResult> Remove(Guid userId)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			await _userManager.DeleteAsync(user);

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult ChangePassword(Guid userId)
		{
			ViewBag.UserId = userId;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePassword changes)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(ChangePassword));
			}

			var user = await _userManager.FindByIdAsync(changes.UserId.ToString());
			var newHashPassword = _userManager.PasswordHasher.HashPassword(user, changes.Password);
			user.PasswordHash = newHashPassword;
			await _userManager.UpdateAsync(user);

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid userId)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			var model = _mapper.Map<UserViewModel>(user);
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UserViewModel newUser)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var currentUser = await _userManager.FindByIdAsync(newUser.Id);

			currentUser.PhoneNumber = newUser.PhoneNumber;
			currentUser.UserName = newUser.UserName;
			await _userManager.UpdateAsync(currentUser);

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> EditRights(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			var userRoles = await _userManager.GetRolesAsync(user);
			var roles = _roleManager.Roles.ToList();

			return View(new EditRightsViewModel
			{
				UserId = user.Id,
				UserName = user.UserName,
				UserRoles = userRoles.Select(x => new RoleViewModel { Name = x }).ToList(),
				AllRoles = roles.Select(x => new RoleViewModel { Name = x.Name }).ToList()
			});
		}

		[HttpPost]
		public async Task<IActionResult> EditRights(string userId, Dictionary<string, string> userRolesViewModel) 
		{
			var userSelectedRoles = userRolesViewModel.Select(x => x.Key);

			var user = await _userManager.FindByIdAsync(userId);
			var userRoles = await _userManager.GetRolesAsync(user);

			await _userManager.RemoveFromRolesAsync(user, userRoles);
			await _userManager.AddToRolesAsync(user, userSelectedRoles);

			return RedirectToAction("EditRights", new { userId = userId });
		}
	}
}
