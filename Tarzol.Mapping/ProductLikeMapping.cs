using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Entity;

namespace Tarzol.Mapping
{
    public class ProductLikeMapping : IEntityTypeConfiguration<ProductLike>
    {
        public void Configure(EntityTypeBuilder<ProductLike> builder)
        {
            builder.HasOne(i => i.Product).WithMany(i => i.ProductLikes).HasForeignKey(i => i.ProductID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.Customer).WithMany(i => i.ProductLikes).HasForeignKey(i => i.CustomerID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
