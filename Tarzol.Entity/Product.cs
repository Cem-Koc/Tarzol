using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal MarketPrice { get; set; }
        public int UnitsInStock { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string TradeMark { get; set; }
        public string ProductCode { get; set; }
        public int CargoID { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int ProductColorID { get; set; }
        public int ProductSizeID { get; set; }
        public int SellerID { get; set; }
        public string Description { get; set; }
        public string ProductInformation { get; set; }

        public string ImageOne { get; set; }
        public string ImageTwo { get; set; }
        public string ImageThree { get; set; }
        public string ImageFour { get; set; }



        public virtual Seller Seller { get; set; }
        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual Cargo Cargo { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

        public virtual List<Question> Questions { get; set; }
        public virtual List<ProductRating> ProductRatings { get; set; }
        public virtual List<ProductLike> ProductLikes { get; set; }
        
        public virtual ProductColor ProductColor { get; set; }
        public virtual ProductSize ProductSize { get; set; }
    }
}
