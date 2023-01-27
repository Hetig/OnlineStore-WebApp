using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.VeiwModels
{
    public enum OrderStatusesViewModel
    {
        [Display(Name = "Создан")]
        Created,
        [Display(Name = "Обработан")]
        Processed,
        [Display(Name = "В пути")]
        Sent,
        [Display(Name = "Отменен")]
        Canceled,
        [Display(Name = "Доставлен")]
        Delivered
    }
}
