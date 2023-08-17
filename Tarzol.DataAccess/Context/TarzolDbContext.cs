using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tarzol.Entity;
using Tarzol.Mapping;

namespace Tarzol.DataAccess.Context
{
   public class TarzolDbContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public TarzolDbContext(DbContextOptions<TarzolDbContext> option) : base(option)
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<ProductLike> ProductLikes { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<CategoryAndSubCategory> CategoryAndSubCategories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Todo> Todos { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            



            new AnswerMapping().Configure(builder.Entity<Answer>());
            new OrderDetailMapping().Configure(builder.Entity<OrderDetail>());
            new OrderMapping().Configure(builder.Entity<Order>());
            new ProductMapping().Configure(builder.Entity<Product>());
            new CustomerMapping().Configure(builder.Entity<Customer>());
            new SellerMapping().Configure(builder.Entity<Seller>());
            new ProductLikeMapping().Configure(builder.Entity<ProductLike>());
            new CategoryAndSubCategoryMapping().Configure(builder.Entity<CategoryAndSubCategory>());
            new OrderDetailMapping().Configure(builder.Entity<OrderDetail>());
           
            


        }
    }
}
