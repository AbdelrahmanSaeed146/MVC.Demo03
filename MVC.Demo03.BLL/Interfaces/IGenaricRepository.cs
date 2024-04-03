using MVC.Demo03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Demo03.BLL.Interfaces
{
    public interface IGenaricRepository<T> where T : ModelBase
    {

        Task< IEnumerable<T>> GetAll();

        Task<T> Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);



    }
}
