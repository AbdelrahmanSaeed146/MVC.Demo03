using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC.Demo03.BLL.Interfaces;
using MVC.Demo03.BLL.Repositories;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;


        public EmployeeController ( IUnitOfWork unitOfWork, IMapper mapper , IWebHostEnvironment env )
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }


        public IActionResult Index( string SearchInput)
        {
            var employee = Enumerable.Empty<Employee>();
            var EmpRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;

            if (string.IsNullOrEmpty(SearchInput))
                 employee = _unitOfWork.Repository<Employee>().GetAll();
            else
                 employee = EmpRepo.SearchByName(SearchInput.ToLower());

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

                _unitOfWork.Repository<Employee>().Add(mappedEmp);

                var count = _unitOfWork.Complete();

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


            var Employee = _unitOfWork.Repository<Employee>().Get(id.Value);
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

                _unitOfWork.Repository<Employee>().Update(mappedEmp);
                _unitOfWork.Complete();
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

                _unitOfWork.Repository<Employee>().Delete(mappedEmp);
                _unitOfWork.Complete();
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
