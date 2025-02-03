using Brain_Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain_DAL.Data.configure
{
    public class AdminConfigure : IEntityTypeConfiguration<Admin>
    {
       
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(x => x.SSN);
            builder.Property(x => x.SSN).HasColumnName("SSN").HasMaxLength(14).HasColumnType("varchar").IsRequired(true);
            builder.Property(x => x.Password).HasColumnName("Password").HasMaxLength(50).HasColumnType("varchar").IsRequired(true);
            builder.HasOne(x => x.Employee).WithOne( x => x.Admin).HasForeignKey<Admin>( x => x.SSN).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
