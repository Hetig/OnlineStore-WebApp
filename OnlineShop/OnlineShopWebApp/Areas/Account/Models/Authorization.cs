using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Account.Models
{
    public class Authorization
    {
        [Required(ErrorMessage = "Не указан email")]
        [EmailAddress(ErrorMessage = "Введите существующий email")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
