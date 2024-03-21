using Microsoft.AspNetCore.Mvc;
using MVC.Demo03.BLL.Interfaces;
using MVC.Demo03.BLL.Repositories;

namespace MVC.Demo03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepo.GetAll(); 
            return View(departments);
        }
    }
}
