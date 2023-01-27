using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.VeiwModels
{
    public class EditRightsViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleViewModel> UserRoles { get; set; }
        public List<RoleViewModel> AllRoles { get; set; }
    }
}
