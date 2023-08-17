using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Entity;

namespace Tarzol.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(i => i.ID);
            builder.ToTable("Orders");

            
            builder.Property(i => i.OrderDate).HasColumnName("OrderDate");
            builder.Property(i => i.EstimatedDelivreyDate).HasColumnName("EstimatedDelivreyDate");
            builder.Property(i => i.DelivreyDate).HasColumnName("DelivreyDate");
            builder.Property(i => i.OrderNumber).HasColumnName("OrderNumber");
            builder.Property(i => i.OrderStatus).HasColumnName("OrderStatus");

            builder.HasOne(i => i.Customer).WithMany(i => i.Orders).HasForeignKey(i => i.CustomerID).HasForeignKey(i => i.CustomerID);



        }
    }
}
