using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.VeiwModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
