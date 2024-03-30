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
    public class EmployeeRepository : GenaricRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext) :base(dbContext)
        {
           _dbContext = dbContext;
        }


        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dbContext.Employees.Where(e=> string.Equals(e.Address, address, StringComparison.OrdinalIgnoreCase));
        }

        public IQueryable<Employee> SearchByName(string name)
         => _dbContext.Employees.Where(e => e.Name.ToLower().Contains(name));
    }
}
