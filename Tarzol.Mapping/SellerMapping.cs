using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Entity;

namespace Tarzol.Mapping
{
    public class SellerMapping : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasOne(i => i.District).WithMany(i => i.Sellers).HasForeignKey(i => i.DistrictID).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
