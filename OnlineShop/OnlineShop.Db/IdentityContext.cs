using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace OnlineShop.Db
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        { 
            Database.Migrate();
        }
	}
}
