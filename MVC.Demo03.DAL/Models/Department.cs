using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Demo03.DAL.Models
{
    public class Department :ModelBase
    {


        [Required(ErrorMessage ="Code Is Requierd")]
        public String Code { get; set; }

        [Required(ErrorMessage = "Name Is Requierd")]
        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        //[InverseProperty(nameof(Employee.Department))]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }
}
