using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC.Demo03.BLL.Interfaces;
using MVC.Demo03.DAL.Models;
using System;

namespace MVC.Demo03.PL.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _EmployeeRepo;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepo, IWebHostEnvironment env)
        {
            _EmployeeRepo = employeeRepo;
            _env = env;
        }


        public IActionResult Index()
        {
            var Employees = _EmployeeRepo.GetAll();
            return View(Employees);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee Employee)
        {
            if (ModelState.IsValid) // server sidee validation
            {
                var count = _EmployeeRepo.Add(Employee);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }

        [HttpGet]

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var Employee = _EmployeeRepo.Get(id.Value);

            if (Employee is null)
                return NotFound();

            return View(Employee);
        }

        [HttpGet]

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee dept)
        {

            if (id != dept.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(dept);

            try
            {
                _EmployeeRepo.Update(dept);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1- Log Exception
                // 2- Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error in Employee");

                return View(dept);

            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee dept)
        {
            try
            {
                _EmployeeRepo.Delete(dept);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dept);
            }

        }

    }
}
