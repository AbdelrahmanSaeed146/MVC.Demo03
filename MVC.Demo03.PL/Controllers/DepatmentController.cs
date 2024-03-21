using Microsoft.AspNetCore.Mvc;
using MVC.Demo03.BLL.Interfaces;
using MVC.Demo03.BLL.Repositories;

namespace MVC.Demo03.PL.Controllers
{
    public class DepatmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepatmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
