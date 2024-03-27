using MVC.Demo03.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace MVC.Demo03.PL.Models
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max Length Of Name is 50 chars")]
        [MinLength(5, ErrorMessage = "Min Length Of Name is 5 chars")]
        public string Name { get; set; }

        [Range(22, 30)]
        public int Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
         , ErrorMessage = "Address must be like 123-Street-City-Country")]

        public string Address { get; set; }

        [DataType(DataType.Currency)]

        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]

        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmpType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }

        //[InverseProperty(nameof(Models.Department.Employees))]
        public Department Department { get; set; }
    }
}
