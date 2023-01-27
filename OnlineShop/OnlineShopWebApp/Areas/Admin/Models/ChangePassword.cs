using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class ChangePassword
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать не менее 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не указан повторный пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать не менее 6 символов")]
        public string ConfirmPassword { get; set; }
    }
}
