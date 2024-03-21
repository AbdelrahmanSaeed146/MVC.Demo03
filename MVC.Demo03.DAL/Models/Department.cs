using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Demo03.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Code Is Requierd")]
        public String Code { get; set; }

        [Required(ErrorMessage = "Name Is Requierd")]
        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

    }
}
