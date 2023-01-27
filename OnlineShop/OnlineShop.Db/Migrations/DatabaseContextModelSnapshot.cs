﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.Db;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineShop.Db.Models.Basket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.BasketItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid?>("BasketId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.ComparedProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ComparedProducts");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.FavoriteProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c96dc613-1372-4746-87d7-47fed78a990b"),
                            ProductId = new Guid("399376e5-1ada-486f-acbf-033e6d6ba649"),
                            Url = "/images/Products/image2.jpg"
                        },
                        new
                        {
                            Id = new Guid("68bfe1d6-a659-4407-aa2a-d38b10af42b1"),
                            ProductId = new Guid("735e2ff8-4a9d-417d-e28d-08daddc27f08"),
                            Url = "/images/Products/image6.jpg"
                        },
                        new
                        {
                            Id = new Guid("eff77ecd-0c8d-4396-a3de-a047beb07a46"),
                            ProductId = new Guid("cd71401a-3492-451f-bceb-172e89327423"),
                            Url = "/images/Products/image5.jpg"
                        },
                        new
                        {
                            Id = new Guid("2cceb603-09c7-4094-b68d-60897b3408a8"),
                            ProductId = new Guid("dff9eb75-338d-4bec-acca-536af43f314b"),
                            Url = "/images/Products/image4.jpg"
                        },
                        new
                        {
                            Id = new Guid("7ef36c5d-5fe6-459f-871b-49cca0d21be1"),
                            ProductId = new Guid("2fd73246-ad73-414f-9f29-c7820ec9c3a9"),
                            Url = "/images/Products/image3.jpg"
                        },
                        new
                        {
                            Id = new Guid("156c541d-fa7b-40a5-981f-872e2ddf3ce4"),
                            ProductId = new Guid("c49c6475-b8c9-4cbe-92cb-c9424b07453d"),
                            Url = "/images/Products/image1.jpg"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("399376e5-1ada-486f-acbf-033e6d6ba649"),
                            Cost = 15000m,
                            Description = "Исключительно натуральные материалы",
                            IsFavorite = false,
                            Name = "Кроссовки Nike"
                        },
                        new
                        {
                            Id = new Guid("735e2ff8-4a9d-417d-e28d-08daddc27f08"),
                            Cost = 25000m,
                            Description = "Исключительно натуральные материалы",
                            IsFavorite = false,
                            Name = "Gucci для дома"
                        },
                        new
                        {
                            Id = new Guid("cd71401a-3492-451f-bceb-172e89327423"),
                            Cost = 25000m,
                            Description = "Исключительно натуральные материалы",
                            IsFavorite = false,
                            Name = "Кроссовки Adidas"
                        },
                        new
                        {
                            Id = new Guid("dff9eb75-338d-4bec-acca-536af43f314b"),
                            Cost = 25000m,
                            Description = "Исключительно натуральные материалы",
                            IsFavorite = false,
                            Name = "Туфли Jimmy Choo"
                        },
                        new
                        {
                            Id = new Guid("2fd73246-ad73-414f-9f29-c7820ec9c3a9"),
                            Cost = 25000m,
                            Description = "Исключительно натуральные материалы",
                            IsFavorite = false,
                            Name = "Reebok для дома"
                        },
                        new
                        {
                            Id = new Guid("c49c6475-b8c9-4cbe-92cb-c9424b07453d"),
                            Cost = 25000m,
                            Description = "Исключительно натуральные материалы",
                            IsFavorite = false,
                            Name = "Lacoste мужские"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.UserDeliveryInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserDeliveryInfo");
                });

            modelBuilder.Entity("OnlineShopWebApp.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserDeliveryInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserDeliveryInfoId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.BasketItem", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Basket", null)
                        .WithMany("Items")
                        .HasForeignKey("BasketId");

                    b.HasOne("OnlineShopWebApp.Models.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("Items")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.ComparedProduct", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.FavoriteProduct", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Image", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShopWebApp.Models.Order", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.UserDeliveryInfo", "UserDeliveryInfo")
                        .WithMany()
                        .HasForeignKey("UserDeliveryInfoId");

                    b.Navigation("UserDeliveryInfo");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Basket", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("OnlineShopWebApp.Models.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
