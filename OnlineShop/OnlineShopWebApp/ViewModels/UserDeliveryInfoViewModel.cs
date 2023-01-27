using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.VeiwModels
{
    public class UserDeliveryInfoViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 25 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указано фамилие")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 25 символов")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указан email")]
        [EmailAddress(ErrorMessage = "Введите существующий email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона")]
        public string PhoneNumber { get; set; }

        [StringLength(500, ErrorMessage = "Комментарий должен содержать не более 500 символов")]
        public string Comment { get; set; }
    }
}
