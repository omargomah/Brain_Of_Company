
using Interfaces;
//using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Brain_Entities.Models;

namespace Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository <Attendance> Attendances { get; }

        public IRepository <Category> Categories { get; }

        public IRepository <Department> Departments { get; }

        public IRepository <Dependent> Dependents { get; }

        public IRepository <Dependent_Employee> Dependent_Employees { get; }

        public IRepository <Employee> Employees { get; }

        public IRepository <Invoice> Invoices { get; }

        public IRepository <Product> Products { get; }
        public IRepository <Admin> Admins { get; }
        
        int Save();
    }
}
