using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tarzol.Mapping
{
    public class AnswerMapping : IEntityTypeConfiguration<Answer>
    {

        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(i => i.ID);
            //builder.HasOne(i => i.Question).WithOne(i => i.Answer).HasForeignKey<Answer>(i => i.ID);
            builder.HasOne(i => i.Product).WithMany(i => i.Answers).HasForeignKey(i => i.ProductID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.Seller).WithMany(i => i.Answers).HasForeignKey(i => i.SellerID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
