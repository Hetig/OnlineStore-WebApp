using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.VeiwModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductsStorage _productsStorage;
		private readonly IMapper _mapper;

		public HomeController(IProductsStorage productsStorage, IMapper mapper)
		{
			_productsStorage = productsStorage;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var productsDbList = await _productsStorage.GetAllAsync();
			var models = _mapper.Map<List<Product>, List<ProductViewModel>>(productsDbList);
			return View(models);
		}

		public IActionResult ChangeLanguage(string culture)
		{
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions
				{
					Expires = DateTimeOffset.UtcNow.AddDays(7)
				});

			return RedirectToAction("Index");
		}
	}
}
