using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.VeiwModels;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductsStorage _productsStorage;
		private readonly IMapper _mapper;


		public SearchController(IProductsStorage productsStorage, IMapper mapper)
		{
			_productsStorage = productsStorage;
			_mapper = mapper;
		}

		public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FindProduct(string productName)
        {
            var productsDb = await _productsStorage.FindProductsAsync(productName);
			var models = _mapper.Map<List<ProductViewModel>>(productsDb);

			return View(models);
        }
    }
}
