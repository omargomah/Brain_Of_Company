using Brain_Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
