using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Demo03.DAL.Models
{
    internal class Department
    {
        public int Id { get; set; }
        public String Code { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

    }
}
