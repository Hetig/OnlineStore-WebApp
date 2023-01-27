using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.VeiwModels;
using System;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsStorage _productsStorage;
        private readonly IMapper _mapper;

		public ProductController(IProductsStorage productsStorage, IMapper mapper)
		{
			_productsStorage = productsStorage;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index(Guid id)
        {
            var productDb = await _productsStorage.TryGetByIdAsync(id);
			var model = _mapper.Map<ProductViewModel>(productDb);

			return View(model);
        }
    }
}
