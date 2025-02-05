﻿// <auto-generated />
using System;
using Brain_DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Brain_DAL.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20250205195000_four")]
    partial class four
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Brain_Entities.Models.Admin", b =>
                {
                    b.Property<string>("SSN")
                        .HasMaxLength(14)
                        .HasColumnType("varchar")
                        .HasColumnName("SSN");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("Password");

                    b.HasKey("SSN");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Brain_Entities.Models.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfDay")
                        .HasColumnType("DateTime")
                        .HasColumnName("DateOfDay");

                    b.Property<string>("EmployeeSSN")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar")
                        .HasColumnName("EmployeeSSN");

                    b.Property<bool>("IsAttended")
                        .HasColumnType("BIT")
                        .HasColumnName("IsAttended");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeSSN");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("Brain_Entities.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Brain_Entities.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Location");

                    b.Property<string>("ManagerSSN")
                        .HasMaxLength(14)
                        .HasColumnType("varchar")
                        .HasColumnName("ManagerSSN");

                    b.Property<int>("MinimumDaysToAttendancePerMonth")
                        .HasColumnType("int")
                        .HasColumnName("MinimumDaysToAttendancePerMonth");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("ManagerSSN")
                        .IsUnique()
                        .HasFilter("[ManagerSSN] IS NOT NULL");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Brain_Entities.Models.Dependent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Dependent");
                });

            modelBuilder.Entity("Brain_Entities.Models.Dependent_Employee", b =>
                {
                    b.Property<string>("EmployeeSSN")
                        .HasMaxLength(14)
                        .HasColumnType("varchar")
                        .HasColumnName("EmployeeSSN")
                        .HasColumnOrder(1);

                    b.Property<int>("DependentId")
                        .HasColumnType("int")
                        .HasColumnName("DependentId")
                        .HasColumnOrder(0);

                    b.HasKey("EmployeeSSN", "DependentId");

                    b.HasIndex("DependentId");

                    b.ToTable("Dependent_Employee");
                });

            modelBuilder.Entity("Brain_Entities.Models.Employee", b =>
                {
                    b.Property<string>("SSN")
                        .HasMaxLength(14)
                        .HasColumnType("varchar")
                        .HasColumnName("SSN");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("City");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Country");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("DateTime")
                        .HasColumnName("DateOfBirth");

                    b.Property<DateTime?>("DateOfFiring")
                        .HasColumnType("DateTime")
                        .HasColumnName("DateOfFiring");

                    b.Property<DateTime>("DateOfHiring")
                        .HasColumnType("DateTime")
                        .HasColumnName("DateOfHiring");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int")
                        .HasColumnName("DepartmentId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Email");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Gender");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("BIT")
                        .HasColumnName("Isdeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Name");

                    b.Property<decimal>("PercentageOfBonus")
                        .HasColumnType("decimal")
                        .HasColumnName("PercentageOfBonus");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar")
                        .HasColumnName("Phone");

                    b.Property<decimal>("SalaryPerDay")
                        .HasColumnType("decimal")
                        .HasColumnName("SalaryPerDay");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Street");

                    b.HasKey("SSN");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Brain_Entities.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DOS")
                        .HasColumnType("DateTime")
                        .HasColumnName("DateOfSale");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("BIT")
                        .HasColumnName("Isdeleted");

                    b.Property<decimal>("Price")
                        .HasColumnType("Decimal")
                        .HasColumnName("Price");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<int>("Quantities")
                        .HasColumnType("int")
                        .HasColumnName("Quantities");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Brain_Entities.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryId");

                    b.Property<DateTime>("DOA")
                        .HasColumnType("DateTime")
                        .HasColumnName("DateOfAdd");

                    b.Property<DateTime?>("DOD")
                        .HasColumnType("DateTime")
                        .HasColumnName("DateOfDelete");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("BIT")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("Decimal")
                        .HasColumnName("Price");

                    b.Property<int>("RealQuantities")
                        .HasColumnType("int")
                        .HasColumnName("RealQuantities");

                    b.Property<int>("SoldQuantities")
                        .HasColumnType("int")
                        .HasColumnName("SoldQuantities");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Brain_Entities.Models.Admin", b =>
                {
                    b.HasOne("Brain_Entities.Models.Employee", "Employee")
                        .WithOne("Admin")
                        .HasForeignKey("Brain_Entities.Models.Admin", "SSN")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Brain_Entities.Models.Attendance", b =>
                {
                    b.HasOne("Brain_Entities.Models.Employee", "Employee")
                        .WithMany("Attendances")
                        .HasForeignKey("EmployeeSSN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Brain_Entities.Models.Department", b =>
                {
                    b.HasOne("Brain_Entities.Models.Employee", "ManagedBy")
                        .WithOne("ManagedDepartment")
                        .HasForeignKey("Brain_Entities.Models.Department", "ManagerSSN")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ManagedBy");
                });

            modelBuilder.Entity("Brain_Entities.Models.Dependent_Employee", b =>
                {
                    b.HasOne("Brain_Entities.Models.Dependent", "Dependent")
                        .WithMany("dependent_Employees")
                        .HasForeignKey("DependentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brain_Entities.Models.Employee", "Employee")
                        .WithMany("dependent_Employees")
                        .HasForeignKey("EmployeeSSN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dependent");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Brain_Entities.Models.Employee", b =>
                {
                    b.HasOne("Brain_Entities.Models.Department", "WorkingDepartment")
                        .WithMany("WorkingEmployees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkingDepartment");
                });

            modelBuilder.Entity("Brain_Entities.Models.Invoice", b =>
                {
                    b.HasOne("Brain_Entities.Models.Product", "Product")
                        .WithMany("Invoices")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Brain_Entities.Models.Product", b =>
                {
                    b.HasOne("Brain_Entities.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Brain_Entities.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Brain_Entities.Models.Department", b =>
                {
                    b.Navigation("WorkingEmployees");
                });

            modelBuilder.Entity("Brain_Entities.Models.Dependent", b =>
                {
                    b.Navigation("dependent_Employees");
                });

            modelBuilder.Entity("Brain_Entities.Models.Employee", b =>
                {
                    b.Navigation("Admin")
                        .IsRequired();

                    b.Navigation("Attendances");

                    b.Navigation("ManagedDepartment")
                        .IsRequired();

                    b.Navigation("dependent_Employees");
                });

            modelBuilder.Entity("Brain_Entities.Models.Product", b =>
                {
                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
