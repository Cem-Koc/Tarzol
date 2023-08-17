using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Entity;

namespace Tarzol.Mapping
{
    public class CategoryAndSubCategoryMapping : IEntityTypeConfiguration<CategoryAndSubCategory>
    {
        public void Configure(EntityTypeBuilder<CategoryAndSubCategory> builder)
        {
            builder.HasKey(i => new { i.CategoryID, i.SubCategoryID });
            builder.HasOne(i => i.Category).WithMany(i => i.CategoryAndSubCategories).HasForeignKey(i => i.CategoryID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.SubCategory).WithMany(i => i.CategoryAndSubCategories).HasForeignKey(i => i.SubCategoryID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
