using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db
{
	public class DatabaseContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Basket> Baskets { get; set; }
		public DbSet<BasketItem> Items { get; set; }
		public DbSet<FavoriteProduct> Favorites { get; set; }
		public DbSet<ComparedProduct> ComparedProducts { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Image> Images { get; set; }
		public DatabaseContext(DbContextOptions<DatabaseContext> options)
			: base(options)
		{
			Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Image>().HasOne(x => x.Product).WithMany(x => x.Images).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

			var product1Id = Guid.Parse("399376e5-1ada-486f-acbf-033e6d6ba649");
			var product2Id = Guid.Parse("735e2ff8-4a9d-417d-e28d-08daddc27f08");
			var product3Id = Guid.Parse("cd71401a-3492-451f-bceb-172e89327423");
			var product4Id = Guid.Parse("dff9eb75-338d-4bec-acca-536af43f314b");
			var product5Id = Guid.Parse("2fd73246-ad73-414f-9f29-c7820ec9c3a9");
			var product6Id = Guid.Parse("c49c6475-b8c9-4cbe-92cb-c9424b07453d");

			var image1 = new Image
			{
				Id = Guid.Parse("c96dc613-1372-4746-87d7-47fed78a990b"),
				Url = "/images/Products/image2.jpg",
				ProductId = product1Id,
			};

			var image2 = new Image
			{
				Id = Guid.Parse("68bfe1d6-a659-4407-aa2a-d38b10af42b1"),
				Url = "/images/Products/image6.jpg",
				ProductId = product2Id,
			};

			var image3 = new Image
			{
				Id = Guid.Parse("eff77ecd-0c8d-4396-a3de-a047beb07a46"),
				Url = "/images/Products/image5.jpg",
				ProductId = product3Id,
			};

			var image4 = new Image
			{
				Id = Guid.Parse("2cceb603-09c7-4094-b68d-60897b3408a8"),
				Url = "/images/Products/image4.jpg",
				ProductId = product4Id,
			};

			var image5 = new Image
			{
				Id = Guid.Parse("7ef36c5d-5fe6-459f-871b-49cca0d21be1"),
				Url = "/images/Products/image3.jpg",
				ProductId = product5Id,
			};

			var image6 = new Image
			{
				Id = Guid.Parse("156c541d-fa7b-40a5-981f-872e2ddf3ce4"),
				Url = "/images/Products/image1.jpg",
				ProductId = product6Id,
			};

			modelBuilder.Entity<Image>().HasData(image1, image2, image3, image4, image5, image6);

			modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
	             new Product{ Id = product1Id, Name = "Кроссовки Nike", Cost = 15000, Description = "Исключительно натуральные материалы" },
	             new Product{ Id = product2Id, Name = "Gucci для дома", Cost = 25000, Description = "Исключительно натуральные материалы" },
	             new Product{ Id = product3Id, Name = "Кроссовки Adidas", Cost = 25000, Description = "Исключительно натуральные материалы" },
	             new Product{ Id = product4Id, Name = "Туфли Jimmy Choo", Cost = 25000, Description = "Исключительно натуральные материалы" },
	             new Product{ Id = product5Id, Name = "Reebok для дома", Cost = 25000, Description = "Исключительно натуральные материалы" },
	             new Product{ Id = product6Id, Name = "Lacoste мужские", Cost = 25000, Description = "Исключительно натуральные материалы" }
            });
		}
	}
}