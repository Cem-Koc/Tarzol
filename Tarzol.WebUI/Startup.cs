using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.AutoMapperTier.Mapper;
using Tarzol.Business.Abstract;
using Tarzol.Business.Concrete;
using Tarzol.DataAccess.Abstract;
using Tarzol.DataAccess.Concrete.EfRepository;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;
using Tarzol.WebUI.Identity;

namespace Tarzol.WebUI
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
            services.AddControllersWithViews();
           

            services.AddDbContext<TarzolDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("dbConnection")));
            
            services.AddIdentity<AppUser, AppRole>(x=> 
            {
                x.User.AllowedUserNameCharacters = "abcçdefgðhýijklmnoöprsþtuüvyzABCÇDEFGÐHIÝJKLMNOÖPRSÞTUÜVYZ0123456789-._@+ ";
                x.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<TarzolDbContext>();


            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                
            });
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };


            services.AddTransient<ICategoryRepository, EfCategoryRepository>();
            services.AddTransient<ICategoryService, CategoryManager>();

            services.AddTransient<IProductRepository, EfProductRepository>();
            services.AddTransient<IProductService, ProductManager>();

            services.AddTransient<ISellerRepository, EfSellerRepository>();
            services.AddTransient<ISellerService, SellerManager>();

            services.AddTransient<IQuestionRepository, EfQuestionRepository>();
            services.AddTransient<IQuestionService, QuestionManager>();

            services.AddTransient<IFavoriteRepository, EfFavoriteRepository>();
            services.AddTransient<IFavoriteService, FavoriteManager>();

            services.AddTransient<ICustomerRepository, EfCustomerRepository>();
            services.AddTransient<ICustomerService, CustomerManager>();

            services.AddTransient<IOrderRepository, EfOrderRepository>();
            services.AddTransient<IOrderService, OrderManager>();

            services.AddTransient<IOrderDetailRepository, EfOrderDetailRepository>();
            services.AddTransient<IOrderDetailService, OrderDetailManager>();

            services.AddTransient<IProductRatingRepository, EfProductRatingRepository>();
            services.AddTransient<IProductRatingService, ProductRatingManager>();

            services.AddTransient<ICommentRepository, EfCommentRepository>();
            services.AddTransient<ICommentService, CommentManager>();

            services.AddTransient<INotificationRepository, EfNotificationRepository>();
            services.AddTransient<INotificationService, NotificationManager>();

            services.AddTransient<IMessageRepository, EfMessageRepository>();
            services.AddTransient<IMessageService, MessageManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,RoleManager<AppRole>roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
            MyIdentityDataInitializer.SeedRoles(roleManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(

                   name: "areaDefault",
                   areaName:"Admin",
                   pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

               
            });
        }
    }
}
