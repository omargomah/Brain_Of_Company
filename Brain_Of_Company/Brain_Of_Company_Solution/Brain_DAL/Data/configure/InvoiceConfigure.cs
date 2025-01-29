using Brain_Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain_DAL.Data.configure
{
    internal class InvoiceConfigure : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("int").IsRequired(true);
            builder.Property(x => x.ProductId).HasColumnName("ProductId").HasColumnType("int").IsRequired(true);
            builder.Property(x => x.DOS).HasColumnName("DateOfSale").HasColumnType("DateTime").IsRequired(true);
            builder.Property(x => x.IsDeleted).HasColumnName("Isdeleted").HasColumnType("BIT").IsRequired(true);
            builder.Property(x => x.Quantities).HasColumnName("Quantities").HasColumnType("int").IsRequired(true);
            builder.Property(x => x.Price).HasColumnName("Price").HasColumnType("Decimal").IsRequired(true);
        }
    }
}
