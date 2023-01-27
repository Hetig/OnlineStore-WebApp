using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public enum OrderStatuses
    {
        Created,
        Processed,
        Sent,
        Canceled,
        Delivered
    }
}
