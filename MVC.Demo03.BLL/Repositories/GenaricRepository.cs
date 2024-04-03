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
        private protected readonly AppDbContext _dbcontext;

        public GenaricRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public void  Add(T entity)
            =>  _dbcontext.Set<T>().Add(entity);
          
        

        public void Update(T entity)
         => _dbcontext.Set<T>().Update(entity);


        public void Delete(T entity)
          =>
            _dbcontext.Set<T>().Remove(entity);


        public async Task<T> Get(int id)
        {
            return await _dbcontext.FindAsync<T>(id);
        }

        public async Task <IEnumerable<T>> GetAll()
        {

            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _dbcontext.Employees.Include(e => e.Department).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _dbcontext.Set<T>().AsNoTracking().ToListAsync();
            }
            
        }

    }
}
