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
    internal class ProductConfigure : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x=> x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("int").IsRequired(true);
            builder.Property(x => x.RealQuantities).HasColumnName("RealQuantities").HasColumnType("int").IsRequired(true);
            builder.Property(x => x.SoldQuantities).HasColumnName("SoldQuantities").HasColumnType("int").IsRequired(true);
            builder.Property(x => x.Price).HasColumnName("Price").HasColumnType("Decimal").IsRequired(true);
            builder.Property(x => x.DOA).HasColumnName("DateOfArriving").HasColumnType("DateTime").IsRequired(true);
            builder.Property(x => x.DOD).HasColumnName("DateOfArriving").HasColumnType("DateTime").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted").HasColumnType("BIT").IsRequired(true);
            builder.HasMany(x => x.Invoices).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
