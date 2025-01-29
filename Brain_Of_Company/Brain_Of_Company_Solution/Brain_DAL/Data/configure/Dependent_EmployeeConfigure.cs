using Brain_Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brain_DAL.Data.configure
{
    internal class Dependent_EmployeeConfigure : IEntityTypeConfiguration<Dependent_Employee>
    {
        public void Configure(EntityTypeBuilder<Dependent_Employee> builder)
        {
            builder.HasKey(x => new {x.EmployeeSSN,x.DependentId});
            builder.Property(x => x.EmployeeSSN).HasColumnName("EmployeeSSN").HasColumnType("varchar").HasMaxLength(14).IsRequired(true);
            builder.Property(x => x.DependentId).HasColumnName("DependentId").HasColumnType("int").IsRequired(true);
        }
    }
}
