using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC.Demo03.BLL.Interfaces;
using MVC.Demo03.DAL.Models;
using MVC.Demo03.PL.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC.Demo03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _EmployeeRepo;
        private readonly IWebHostEnvironment _env;
        //private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController( IMapper mapper ,IEmployeeRepository employeeRepo, IWebHostEnvironment env /*, IDepartmentRepository departmentRepo*/)
        {
            _mapper = mapper;
            _EmployeeRepo = employeeRepo;
            _env = env;
            //_departmentRepo = departmentRepo;
        }


        public IActionResult Index( string SearchInput)
        {
            var employee = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(SearchInput))
                 employee = _EmployeeRepo.GetAll();
            else
                 employee = _EmployeeRepo.SearchByName(SearchInput.ToLower());

            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);


            return View(mappedEmp);


        }


        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            //ViewBag.Departments = _departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel EmployeeVM)
        {



            if (ModelState.IsValid) // server sidee validation
            {

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                var count = _EmployeeRepo.Add(mappedEmp);

                if (count > 0)
                    TempData["Message"] = "Employee is Created Successfuly";
                else
                    TempData["Message"] = "Employee Not Added";

                    return RedirectToAction(nameof(Index));

            }
            return View(EmployeeVM);


        }

        [HttpGet]

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();


            var Employee = _EmployeeRepo.Get(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);


            if (Employee is null)
                return NotFound();

            return View(mappedEmp);
        }

        [HttpGet]

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {

            if (id != EmployeeVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(EmployeeVM);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                _EmployeeRepo.Update(mappedEmp);
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

                return View(EmployeeVM);

            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel EmployeeVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                _EmployeeRepo.Delete(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(EmployeeVM);
            }

        }

    }
}
