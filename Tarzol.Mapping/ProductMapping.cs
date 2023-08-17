using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Entity;

namespace Tarzol.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(i => i.DiscountedPrice).HasColumnType("decimal(18,4)");
            builder.Property(i => i.MarketPrice).HasColumnType("decimal(18,4)");
            builder.HasKey(i => i.ID);
            builder.HasOne(i => i.SubCategory).WithMany(i => i.Products).HasForeignKey(i => i.SubCategoryID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
