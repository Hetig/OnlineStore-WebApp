using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Db.Storages;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
using Serilog;
using System;
using System.Globalization;

namespace OnlineShopWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Получаем строку подкления из файла конфигурации
            string connection = Configuration.GetConnectionString("Online_Shop_Hetag");
            //Добавляем контекст MobileContext в качестве сервиса в приложение
            services.AddDbContext<DatabaseContext>(options =>
                 options.UseSqlServer(connection));

            services.AddDbContext<IdentityContext>(options =>
                 options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>(options =>
            {
				options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<IdentityContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.LoginPath = "/Account/Authorization/Index";
                options.LogoutPath = "/Account/Authorization/Exit";
                options.Cookie = new CookieBuilder
                {
                    IsEssential = true
                };
            });

            services.AddTransient<IProductsStorage, ProductsDbStorage>();
            services.AddTransient<IBasketStorage, BasketsDbStorage>();
            services.AddTransient<IFavoritesStorage, FavoritesDbStorage>();
            services.AddTransient<IComparedProductsStorage, ComparedDbProductsStorage>();
            services.AddTransient<IOrdersStorage, OrdersDbStorage>();
            services.AddScoped<IImagesProvider, ImagesProvider>();

            services.AddControllersWithViews().AddViewLocalization();
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.AddAutoMapper(typeof(MappingProfile));

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCulture = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ru-RU")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCulture;
                options.SupportedUICultures = supportedCulture;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
