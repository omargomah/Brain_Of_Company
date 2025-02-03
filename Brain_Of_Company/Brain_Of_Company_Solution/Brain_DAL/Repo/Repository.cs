using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Brain_DAL.Data;
using Interfaces;
using Brain_Entities.Models;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDBContext _context;

        public Repository(AppDBContext context)
        {
            _context = context;
        }
        //public  Task<List<Attendance>> GetAttendanceWithEmployeesByDate(DateOnly date)
        //    => _context.Attendance.Where(x => DateOnly.FromDateTime(x.DateOfDay).CompareTo(date) == 0 ).ToListAsync();
        public Attendance? GetAttendanceWithEmployeesById(int id)
            => _context.Attendance.Where(x => x.Id == id).Include(x=>x.Employee).FirstOrDefault();
        public Dependent? GetDependentWithEmployeesById(int id) 
            => _context.Dependent.Where(x => x.Id == id).Include(x=>x.dependent_Employees).ThenInclude(x=>x.Employee).FirstOrDefault();

        public  Department? GetDepartmentWithEmployeeByDEPIdAsync(int id) =>
            _context.Department
            .Where(x => x.Id == id)
            .Include(x => x.WorkingEmployees)
            .Include(x => x.ManagedBy).FirstOrDefault();
        public List<Department> GetAllDepartmentWithEmployeeAndManager() =>
             _context.Department.ToList();
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<T> GetByCompositeAsync(params object[] keys)
        {
            return await _context.Set<T>().FindAsync(keys);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByNameAsync(string name)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);
        }
        public async Task<Employee?> GetBySSNAsync(string SSN)
            => await _context.Employees.FirstOrDefaultAsync(e => e.SSN == SSN);
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public bool CheckExistbyId(int id)
        {
            return _context.Set<T>().Any(x=> EF.Property<int>(x,"Id") == id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public List<string> GetDistinct(Expression<Func<T, string>> col)
        {
            var distinctValues = _context.Set<T>()
                                           .Select(col.Compile())
                                           .Distinct()
                                           .ToList();

            return distinctValues;
        }
        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.SingleOrDefault(criteria);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.SingleOrDefaultAsync(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(criteria).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take, Expression<Func<T, object>> orderBy = null, bool IsDesc = false)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (!IsDesc)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.ToList();
        }

        /// <summary>
        /// used with Jquery datatable
        /// </summary>
        /// <param name="criteria"> condtions</param>
        /// <param name="sortColumn"> sort columns of datatable</param>
        /// <param name="sortColumnDirection"> Asc or Desc</param>
        /// <param name="skip">for paging </param>
        /// <param name="take">for paging</param>
        /// <returns>The records based on the current page</returns>
        public IEnumerable<T> FindWithFilters(Expression<Func<T, bool>> criteria = null, string sortColumn = null, string sortColumnDirection = null, int? skip = null, int? take = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (criteria != null)
                query = _context.Set<T>().Where(criteria);

            //if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            //{
            //    query = query.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));
            //}

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.ToList();
        }


        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
        {
            return await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip, Expression<Func<T, object>> orderBy = null, bool IsDesc = false)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (!IsDesc)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity)
        {
            _context.Update(entity);

            return entity;
        }
        public bool UpdateRange(IEnumerable<T> entities)
        {
            _context.UpdateRange(entities);

            return true;
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }


        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Count(criteria);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }
        public async Task<Int64> MaxAsync(Expression<Func<T, object>> column)
        {
            return Convert.ToInt64(await _context.Set<T>().MaxAsync(column));
        }
        public async Task<Int64> MaxAsync(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> column)
        {
            return Convert.ToInt64(await _context.Set<T>().Where(criteria).MaxAsync(column));
        }
        public Int64 Max(Expression<Func<T, object>> column)
        {
            return Convert.ToInt64(_context.Set<T>().Max(column));
        }
        public Int64 Max(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> column)
        {
            return Convert.ToInt64(_context.Set<T>().Where(criteria).Max(column));
        }
        public bool IsExist(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Any(criteria);
        }
        public bool IsEmployeeExistBySSN(string SSN) =>  _context.Set<Employee>().Any(e=>string.Equals(e.SSN ,SSN) && e.IsDeleted == false);
        public T Last(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy)
        {
            return _context.Set<T>().OrderByDescending(orderBy).FirstOrDefault(criteria);
        }
        public Task<List<Attendance>> GetAttendanceByEmployeesSSN(string SSN)
           => _context.Attendance.Where(x => x.EmployeeSSN == SSN).ToListAsync();
        public Task<List<Attendance>> GetAttendanceByEmployeesSSNInRange(string SSN, DateOnly startDate, DateOnly endDate)
            => _context.Attendance.Where(x => x.EmployeeSSN == SSN && DateOnly.FromDateTime(x.DateOfDay).CompareTo(startDate) >= 0 && DateOnly.FromDateTime(x.DateOfDay).CompareTo(endDate) <= 0).ToListAsync();
        public Attendance? GetAttendanceByEmployeesSSNInSpecificDay(string SSN, DateOnly date)
            => _context.Attendance.FirstOrDefault(x => DateOnly.FromDateTime(x.DateOfDay).CompareTo(date) == 0 && x.EmployeeSSN == SSN);
        public IEnumerable<Dependent_Employee> GetAllDependentEmployeeWithDependentBySSN(string SSN)
            => _context.Dependent_Employee.Where(x => x.EmployeeSSN.Equals(SSN)).Include(x => x.Dependent);
        public Task<List<Attendance>> GetAttendanceWithEmployeesByDate(DateOnly date)
            => _context.Attendance.Where(x => DateOnly.FromDateTime(x.DateOfDay).CompareTo(date) == 0).ToListAsync();

        public bool IsAdminExistBySSN(string SSN) => _context.Set<Admin>().Any(e => string.Equals(e.SSN, SSN));
    }
}

