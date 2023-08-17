using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Entity;

namespace Tarzol.Mapping
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(i => i.ID);
            builder.ToTable("Customers");
            //builder.HasMany(i => i.Orders).WithOne(i => i.Customer).HasForeignKey(i => i.CustomerID);

            //builder.HasOne(i => i.Address).WithOne(i => i.Customer).HasForeignKey<Address>(i => i.ID);
            //builder.HasMany(i => i.Addresses).WithOne(i => i.Customer).HasForeignKey(i => i.CustomerID);
            //builder.HasOne(i => i.AppUser).WithOne(i => i.Customer).HasForeignKey<AppUser>(i => i.CustomerID);
            builder.HasOne(i => i.District).WithMany(i => i.Customers).HasForeignKey(i => i.DistrictID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
