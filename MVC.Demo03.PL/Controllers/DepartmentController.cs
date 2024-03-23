using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC.Demo03.BLL.Interfaces;
using MVC.Demo03.BLL.Repositories;
using MVC.Demo03.DAL.Models;
using System;

namespace MVC.Demo03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepo , IWebHostEnvironment env)
        {
            _departmentRepo = departmentRepo;
            _env = env;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepo.GetAll(); 
            return View(departments);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) // server sidee validation
            {
                var count = _departmentRepo.Add(department);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        [HttpGet]

        public IActionResult Details(int? id , string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var department = _departmentRepo.Get(id.Value);

            if (department is null)
                   return NotFound();
            
            return View(department);
        }

        [HttpGet]

        public IActionResult Edit(int? id) 
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit( [FromRoute] int id , Department dept)
        {

            if (id != dept.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(dept);

            try
            {
                _departmentRepo.Update(dept);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1- Log Exception
                // 2- Friendly Message
                if(_env.IsDevelopment())
                ModelState.AddModelError(string.Empty , ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error in Department");

                return View(dept);

            }
        }
    }
}
