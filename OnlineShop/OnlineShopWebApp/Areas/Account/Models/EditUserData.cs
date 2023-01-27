using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Account.Models
{
	public class EditUserData
	{
		[StringLength(25, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 25 символов")]
		public string FirstName { get; set; }

		[StringLength(25, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 25 символов")]
		public string LastName { get; set; }

		public string Address { get; set; }

		public string PhoneNumber { get; set; }

		public IFormFile UploadedFile { get; set; }
	}
}
