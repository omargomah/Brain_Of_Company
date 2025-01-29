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
    internal class AttendanceConfigure : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("int").IsRequired(true);
            builder.Property(x => x.DateOfDay).HasColumnName("DateOfDay").HasColumnType("DateTime").IsRequired(true);
            builder.Property(x => x.IsAttended).HasColumnName("IsAttended").HasColumnType("BIT").IsRequired(true);
            builder.Property(x => x.EmployeeSSN).HasColumnName("EmployeeSSN").HasMaxLength(14).HasColumnType("varchar").IsRequired(true);
        }
    }
}
