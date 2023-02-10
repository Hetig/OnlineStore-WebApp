using AutoMapper;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.VeiwModels;
using System.Linq;

namespace OnlineShopWebApp.Helpers
{
	public class MappingProfile : Profile
	{
		/// <summary>
		/// В данном классе использовал AutoMapper для преобразования DBModel в ViewModel.
		/// Автомаппер сильно облегчил мне жизнь, поэтому ставлю его в список тех классов которыми я действительно доволен.
		/// </summary>
		public MappingProfile()
		{
			CreateMap<User, UserViewModel>().ReverseMap();
			CreateMap<UserDeliveryInfo, UserDeliveryInfoViewModel>().ReverseMap();

			
			CreateMap<Product, ProductViewModel>()
				     .ForMember(x => x.ImagesPaths, opt => opt.MapFrom(p => p.Images.Select(x => x.Url).ToList()));
			CreateMap<ProductViewModel, Product>()
					 .ForMember(x => x.Images, opt => opt.MapFrom(p => p.ImagesPaths.Select(x => new Image { Url = x }).ToList()));

			CreateMap<AddProductViewModel, Product>()
					 .ForMember(x => x.Images, opt => opt.MapFrom(p => p.ImagesPaths.Select(x => new Image { Url = x }).ToList()));
			CreateMap<EditProductViewModel, Product>()
					 .ForMember(x => x.Images, opt => opt.MapFrom(p => p.ImagesPaths.Select(x => new Image { Url = x }).ToList()));
			CreateMap<Product, EditProductViewModel>()
					 .ForMember(x => x.ImagesPaths, opt => opt.MapFrom(p => p.Images.Select(x => x.Url).ToList()));


			CreateMap<Role, RoleViewModel>().ReverseMap();

			CreateMap<OrderStatuses, OrderStatusesViewModel>().ReverseMap();
			CreateMap<Order, OrderViewModel>().ReverseMap();

			CreateMap<Basket, BasketViewModel>().ReverseMap();
			CreateMap<BasketItem, BasketItemViewModel>().ReverseMap();
		}
	}
}
