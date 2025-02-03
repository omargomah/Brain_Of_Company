using Brain_Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace Brain_DAL.Data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> optionsBuilder) : base(optionsBuilder) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Dependent> Dependent { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Invoice> Invoice { get; set; }       
        public DbSet<Admin> Admin  { get; set; }       
        public DbSet<Dependent_Employee> Dependent_Employee { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Department>(entity =>
        //    {
        //        entity.HasOne(d => d.ManagedBy) // Department has one Employee (ManagedBy)
        //            .WithOne(e => e.ManagedDepartment) // Employee can manage many Departments
        //            .HasForeignKey<Department>(d => d.ManagerSSN); // Foreign key in Department table
        //    });
        //}

    }
}
