﻿using Brain_Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain_DAL.Data.configure
{
    internal class EmployeeConfigure : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.SSN);
            builder.Property(x => x.SSN).HasColumnName("SSN").HasMaxLength(14).HasColumnType("int");
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Street).HasColumnName("Street").HasColumnType("varchar").HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Country).HasColumnName("Country").HasColumnType("varchar").HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Phone).HasColumnName("Phone").HasColumnType("Phone").HasMaxLength(11).IsRequired(true);
            builder.Property(x => x.DepartmentId).HasColumnName("DepartmentId").HasColumnType("int").IsRequired(true);
            builder.Property(x => x.DateOfBirth).HasColumnName("DateOfBirth").HasColumnType("DateOfBirth").IsRequired(true);
            builder.Property(x => x.DateOfHiring).HasColumnName("DateOfHiring").HasColumnType("DateOfHiring").IsRequired(true);
            builder.Property(x => x.DateOfFiring).HasColumnName("DateOfFiring").HasColumnType("DateOfFiring").IsRequired(true);
            builder.Property(x => x.SalaryPerDay).HasColumnName("SalaryPerDay").HasColumnType("decimal").IsRequired(true);
            builder.Property(x => x.PercentageOfBonus).HasColumnName("PercentageOfBonus").HasColumnType("decimal").IsRequired(true);
            builder.Property(x => x.IsDeleted).HasColumnName("Isdeleted").HasColumnType("BIT").IsRequired(true);
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("varchar").IsRequired(true);
            builder.Property(x => x.Gender).HasColumnName("Gender").HasColumnType("varchar").IsRequired(true);
            builder.HasMany(x => x.Attendances).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeSSN).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Dependents).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeSSN).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.ManagedDepartment).WithOne(x => x.ManagedBy).HasForeignKey<Department>(x => x.ManagerSSN).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.WorkingDepartment).WithMany(x => x.WorkingEmployees).HasForeignKey(x => x.DepartmentId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
         
        }
    }
}
