﻿using MVC.Demo03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Demo03.BLL.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {


        IGenaricRepository<T> Repository<T>() where  T : ModelBase;

        int Complete();
       
    }
}
