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
    internal class DepartmentRepository : IDepartmentRepository
    {


        private readonly AppDbContext _DbContext;

        public DepartmentRepository(AppDbContext dbcontext)
        {
            _DbContext = dbcontext;
        }


        public int Add(Department entity)
        {
           _DbContext.Departments.Add(entity);
            return _DbContext.SaveChanges();    
        }

        public int Update(Department entity)
        {
            _DbContext.Departments.Update(entity);
            return _DbContext.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _DbContext.Departments.Remove(entity);
            return _DbContext.SaveChanges();
        }

        public Department Get(int id)
        {
            return _DbContext.Departments.Find(id);
        }

        public IEnumerable<Department> GetAll()
       => _DbContext.Departments.AsNoTracking().ToList();

    
    }
}
