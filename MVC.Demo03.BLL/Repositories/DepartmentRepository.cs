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
    public class DepartmentRepository : GenaricRepository<Department>  ,IDepartmentRepository
    {


        public DepartmentRepository(AppDbContext dbcontext) : base(dbcontext)
        {

        }

    }
}
