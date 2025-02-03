using Brain_Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Interfaces 
{
    public interface IRepository<T> where T : class
    {

        public Task<List<Attendance>> GetAttendanceByEmployeesSSN(string SSN);
        public Task<List<Attendance>> GetAttendanceByEmployeesSSNInRange(string SSN, DateOnly startDate, DateOnly endDate);
        public Attendance? GetAttendanceByEmployeesSSNInSpecificDay(string SSN, DateOnly date);
        public IEnumerable<Dependent_Employee> GetAllDependentEmployeeWithDependentBySSN(string SSN);
        public Task<List<Attendance>> GetAttendanceWithEmployeesByDate(DateOnly date);
        public List<Department> GetAllDepartmentWithEmployeeAndManager();
        T GetById(int id);
        Dependent? GetDependentWithEmployeesById(int id);
        Attendance? GetAttendanceWithEmployeesById(int id);
        Task<T> GetByIdAsync(int id);
        public Department? GetDepartmentWithEmployeeByDEPIdAsync(int id);
        Task<T> GetByCompositeAsync(params object[] keys);
        Task<T> GetByNameAsync(string name);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        public List<string> GetDistinct(Expression<Func<T, string>> col);
        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int take, int skip);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, bool IsDesc = false);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, bool IsDesc = false);

        IEnumerable<T> FindWithFilters(
        Expression<Func<T, bool>> criteria = null,
        string sortColumn = null,
        string sortColumnDirection = null,
        int? skip = null,
        int? take = null);

        T Add(T entity);
        Task<T> AddAsync(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Update(T entity);
        bool UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);

        Task<Int64> MaxAsync(Expression<Func<T, object>> column);

        Task<Int64> MaxAsync(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> column);

        Int64 Max(Expression<Func<T, object>> column);

        Int64 Max(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> column);
        public bool IsExist(Expression<Func<T, bool>> criteria);
        T Last(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy);
        bool IsEmployeeExistBySSN(string SSN);
        bool IsAdminExistBySSN(string SSN);
        public Task<Employee?> GetBySSNAsync(string SSN);

    }
}
