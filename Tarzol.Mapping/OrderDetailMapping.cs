using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Entity;

namespace Tarzol.Mapping
{
    public class OrderDetailMapping : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(i => i.UnitPrice).HasColumnType("decimal(18,4)");
            builder.HasKey(i => new { i.OrderID, i.ProductID });
            builder.HasOne(i => i.Order).WithMany(i => i.OrderDetails).HasForeignKey(i => i.OrderID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.Product).WithMany(i => i.OrderDetails).HasForeignKey(i => i.ProductID).OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
