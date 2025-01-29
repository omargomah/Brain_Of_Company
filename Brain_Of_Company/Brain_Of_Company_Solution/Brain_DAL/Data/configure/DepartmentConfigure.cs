using Brain_Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brain_DAL.Data.configure
{
    internal class DepartmentConfigure : IEntityTypeConfiguration<Department>
        {
            public void Configure(EntityTypeBuilder<Department> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("int").IsRequired(true);
                builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar").IsRequired(true);
                builder.Property(x => x.MinimumDaysToAttendancePerMonth).HasColumnName("MinimumDaysToAttendancePerMonth").HasColumnType("int").IsRequired(true);
                builder.Property(x => x.ManagerSSN).HasColumnName("ManagerSSN").HasColumnType("varchar").HasMaxLength(14).IsRequired(true);
                builder.Property(x => x.Location).HasColumnName("Location").HasColumnType("varchar").IsRequired(true);
            }
        }

}
