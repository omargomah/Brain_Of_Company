using Brain_DAL.Data;
using Interfaces;
using Brain_Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        protected AppDBContext _context;
        public UnitOfWork(AppDBContext context)
        {
            _context = context;
            Attendances = new Repository<Attendance>(_context);
            Categories = new Repository<Category>(_context);
            Departments = new Repository<Department>(_context);
            Dependents = new Repository<Dependent>(_context);
            Dependent_Employees = new Repository<Dependent_Employee>(_context);
            Employees = new Repository<Employee>(_context);
            Invoices = new Repository<Invoice>(_context);
            Products = new Repository<Product>(_context);
            Admins = new Repository<Admin>(_context);

        }
        public IRepository<Attendance> Attendances { get; }

        public IRepository<Category> Categories { get; }

        public IRepository<Department> Departments { get; }

        public IRepository<Dependent> Dependents { get; }

        public IRepository<Dependent_Employee> Dependent_Employees { get; }

        public IRepository<Employee> Employees { get; }

        public IRepository<Invoice> Invoices { get; }
        public IRepository<Admin> Admins { get; }

        public IRepository<Product> Products { get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
