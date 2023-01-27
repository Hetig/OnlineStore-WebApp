using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.VeiwModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constans.AdminRoleName)]
    [Authorize(Roles = Constans.AdminRoleName)]
    public class OrdersController : Controller
    {
        private readonly IOrdersStorage _ordersStorage;
		private readonly IMapper _mapper;


		public OrdersController(IOrdersStorage ordersStorage, IMapper mapper)
		{
			_ordersStorage = ordersStorage;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
        {
            var ordersAsync = await _ordersStorage.GetAllAsync();
            var orders = ordersAsync.Where(x => !x.Items.Select(b => b.Product).Contains(null)).ToList();
            var models = _mapper.Map<List<OrderViewModel>>(orders);
			return View(models);
        }

        public async Task<IActionResult> Order(Guid orderId)
        {
            var order = await _ordersStorage.TryGetByIdAsync(orderId);
            var model = _mapper.Map<OrderViewModel>(order);
            return View(model);
        }

        public async Task<IActionResult> ChangeStatusAsync(OrderStatusesViewModel status, Guid orderId)
        {
            await _ordersStorage.ChangeOrderStatusAsync((OrderStatuses)status, orderId);

            var order = await _ordersStorage.TryGetByIdAsync(orderId);
            var model = _mapper.Map<OrderViewModel>(order);
            return View("Order", model);
        }
    }
}
