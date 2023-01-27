using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.VeiwModels;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constans.AdminRoleName)]
	[Authorize(Roles = Constans.AdminRoleName)]
	public class RolesController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RolesController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			var roles = _roleManager.Roles.ToList();
			return View(roles.Select(x => new RoleViewModel { Name = x.Name }).ToList());
		}

		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(RoleViewModel role)
		{
			var foundRole = await _roleManager.CreateAsync(new IdentityRole(role.Name));
			if (foundRole.Succeeded)
			{
				return RedirectToAction("Index");
			}

			foreach(var error in foundRole.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(role);
		}

		public async Task<IActionResult> Remove(string roleName)
		{
			var foundRole = await _roleManager.FindByNameAsync(roleName);
			if (foundRole != null)
			{
				_roleManager.DeleteAsync(foundRole).Wait();
			}

			return RedirectToAction("Index");
		}
	}
}
