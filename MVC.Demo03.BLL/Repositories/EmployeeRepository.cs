using Microsoft.EntityFrameworkCore;
using MVC.Demo03.BLL.Interfaces;
using MVC.Demo03.DAL.Data;
using MVC.Demo03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Demo03.BLL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _DbContext;

        public EmployeeRepository(AppDbContext dbcontext)
        {
            _DbContext = dbcontext;
        }


        public int Add(Employee entity)
        {
            _DbContext.Employees.Add(entity);
            return _DbContext.SaveChanges();
        }

        public int Update(Employee entity)
        {
            _DbContext.Employees.Update(entity);
            return _DbContext.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            _DbContext.Employees.Remove(entity);
            return _DbContext.SaveChanges();
        }

        public Employee Get(int id)
        {
            return _DbContext.Employees.Find(id);
        }

        public IEnumerable<Employee> GetAll()
       => _DbContext.Employees.AsNoTracking().ToList();


    }
}
