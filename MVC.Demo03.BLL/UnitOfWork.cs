using MVC.Demo03.BLL.Interfaces;
using MVC.Demo03.BLL.Repositories;
using MVC.Demo03.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Demo03.BLL
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly AppDbContext _dbcontext;

 
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public UnitOfWork(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            EmployeeRepository = new EmployeeRepository(_dbcontext);
            DepartmentRepository = new DepartmentRepository(_dbcontext);
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
