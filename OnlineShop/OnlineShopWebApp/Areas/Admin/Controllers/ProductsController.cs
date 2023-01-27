using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.VeiwModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
	[Area(Constans.AdminRoleName)]
	[Authorize(Roles = Constans.AdminRoleName)]
	public class ProductsController : Controller
	{
		private readonly IProductsStorage _productsStorage;
		private readonly IImagesProvider _imagesProvider;
		private readonly IMapper _mapper;

		public ProductsController(IProductsStorage productsStorage, IImagesProvider imagesProvider, IMapper mapper)
		{
			_productsStorage = productsStorage;
			_imagesProvider = imagesProvider;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var productsDbList = await _productsStorage.GetAllAsync();
			var models = _mapper.Map<List<Product>, List<ProductViewModel>>(productsDbList);

			return View(models);
		}

		public async Task<IActionResult> Product(Guid productId)
		{
			var productDb = await _productsStorage.TryGetByIdAsync(productId);
			var model = _mapper.Map<ProductViewModel>(productDb);

			return View(model);
		}

		public async Task<IActionResult> Remove(Guid productId)
		{
			await _productsStorage.RemoveByIdAsync(productId);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddProductViewModel productViewModel)
		{
			if(productViewModel.UploadedFiles == null)
			{
				ModelState.AddModelError("", "Для добавления товара, выберите фотографии");
			}

			if (!ModelState.IsValid)
			{
				return RedirectToAction();
			}

			productViewModel.ImagesPaths = _imagesProvider.SafeFiles(productViewModel.UploadedFiles, ImageFolders.Products);

			await _productsStorage.AppendAsync(_mapper.Map<Product>(productViewModel));
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid productId)
		{
			var productDb = await _productsStorage.TryGetByIdAsync(productId);
			return View(_mapper.Map<EditProductViewModel>(productDb));
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditProductViewModel productViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(productViewModel);
			}

			if (productViewModel.UploadedFiles != null)
			{
				productViewModel.ImagesPaths = _imagesProvider.SafeFiles(productViewModel.UploadedFiles, ImageFolders.Products);
			}

			await _productsStorage.ChangeProductAsync(_mapper.Map<Product>(productViewModel));

			return RedirectToAction("Index");
		}
	}
}
