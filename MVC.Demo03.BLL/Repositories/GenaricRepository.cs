using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class GenaricRepository<T> : IGenaricRepository<T> where T : ModelBase
    {
        private protected readonly AppDbContext _DbContext;

        public GenaricRepository(AppDbContext dbcontext)
        {
            _DbContext = dbcontext;
        }


        public int Add(T entity)
        {
            _DbContext.Set<T>().Add(entity);
            return _DbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _DbContext.Set<T>().Update(entity);
            return _DbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _DbContext.Set<T>().Remove(entity);
            return _DbContext.SaveChanges();
        }

        public T Get(int id)
        {
            return _DbContext.Find<T>(id);
        }

        public IEnumerable<T> GetAll()
        {

            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _DbContext.Employees.Include(e => e.Department).AsNoTracking().ToList();
            }
            else
            {
                return _DbContext.Set<T>().AsNoTracking().ToList();
            }
            
        }

    }
}
