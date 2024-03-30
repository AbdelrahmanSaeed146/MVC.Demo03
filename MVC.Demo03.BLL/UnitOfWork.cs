using MVC.Demo03.BLL.Interfaces;
using MVC.Demo03.BLL.Repositories;
using MVC.Demo03.DAL.Data;
using MVC.Demo03.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Demo03.BLL
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly AppDbContext _dbcontext;
        //private Dictionary<string, IGenaricRepository<ModelBase>> _repos;
        private Hashtable _repos;
 


        public UnitOfWork(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _repos = new Hashtable();

        }


        public IGenaricRepository<T> Repository<T>() where T : ModelBase
        {

            var key = typeof(T).Name;
            if (!_repos.ContainsKey(key))
            {
              
                if (key == nameof(Employee))
                {
                    var repositry = new EmployeeRepository(_dbcontext);
                    _repos.Add(key, repositry);

                }
                else
                {
                    var repositry = new GenaricRepository<T>(_dbcontext);
                    _repos.Add(key, repositry);
                }

            }

            return _repos[key] as IGenaricRepository<T>;
        }


        public int Complete()
        {
           return _dbcontext.SaveChanges();
        }
        public void Dispose()
        {
            _dbcontext.Dispose();
        }

    }
}
