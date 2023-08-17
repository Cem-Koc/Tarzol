using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Entity;

namespace Tarzol.Mapping
{
   public class ProductRatingMapping : IEntityTypeConfiguration<ProductRating>
    {
        public void Configure(EntityTypeBuilder<ProductRating> builder)
        {
            
            builder.HasOne(i => i.Order).WithMany(i => i.ProductRatings).HasForeignKey(i => i.OrderID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.Product).WithMany(i => i.ProductRatings).HasForeignKey(i => i.ProductID).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
