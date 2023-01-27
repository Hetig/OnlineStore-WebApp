using System;

namespace OnlineShop.Db.Models
{
    public class UserDeliveryInfo
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
    }
}
