using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.VeiwModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Введите название роли")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var role = (RoleViewModel)obj;
            return Name.Equals(role.Name);
        }

		public override int GetHashCode()
		{
			return HashCode.Combine(Name);
		}
	}
}
