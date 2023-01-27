using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
		public string AvatarImageUrl { get; set; } = "/images/Profiles/default-user.png";
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }

	}
}
