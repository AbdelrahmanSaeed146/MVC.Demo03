﻿using MVC.Demo03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Demo03.BLL.Interfaces
{
    public interface IGenaricRepository<T> where T : ModelBase
    {

        IEnumerable<T> GetAll();

        T Get(int id);
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);



    }
}
