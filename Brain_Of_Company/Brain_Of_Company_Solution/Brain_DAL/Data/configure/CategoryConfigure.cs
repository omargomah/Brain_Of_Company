using Brain_Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain_DAL.Data.configure
{
    internal class CategoryConfigure : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id); 
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("int").IsRequired(true);
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(100).IsRequired(true);
            builder.HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
