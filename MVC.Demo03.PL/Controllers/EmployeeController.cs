using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC.Demo03.BLL.Interfaces;
using MVC.Demo03.BLL.Repositories;
using MVC.Demo03.DAL.Models;
using MVC.Demo03.PL.Helpers;
using MVC.Demo03.PL.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        public async Task<IActionResult> Index( string SearchInput)
        {
            var employee = Enumerable.Empty<Employee>();
            var EmpRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;

            if (string.IsNullOrEmpty(SearchInput))
                 employee = await _unitOfWork.Repository<Employee>().GetAll();
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
        public async Task< IActionResult> Create(EmployeeViewModel EmployeeVM)
        {



            if (ModelState.IsValid) // server sidee validation
            {
               EmployeeVM.ImageName = await  DocumentSetting.UploadFile(EmployeeVM.image, "Images");


                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

           

                _unitOfWork.Repository<Employee>().Add(mappedEmp);

                var count = await _unitOfWork.Complete();

            

                if (count > 0)
                {

                    return RedirectToAction(nameof(Index));
                }


            }
            return View(EmployeeVM);


        }

        [HttpGet]

        public async Task <IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();


            var Employee = await _unitOfWork.Repository<Employee>().Get(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);


            if (Employee is null)
                return NotFound();
            if (ViewName.Equals("Delete" ,StringComparison.OrdinalIgnoreCase )) 
            TempData["ImageName"] = Employee.imageName;
            

            return View(mappedEmp);
        }

        [HttpGet]

        public async Task<IActionResult>  Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {

            if (id != EmployeeVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(EmployeeVM);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                _unitOfWork.Repository<Employee>().Update(mappedEmp);
                await _unitOfWork.Complete();
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
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel EmployeeVM)
        {
            try
            {

                EmployeeVM.ImageName = TempData["ImageName"] as string;
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                _unitOfWork.Repository<Employee>().Delete(mappedEmp);
               var count = await _unitOfWork.Complete();
                if (count > 0)
                {
                    DocumentSetting.DeleteFile(EmployeeVM.ImageName, "Images");
                return RedirectToAction(nameof(Index));
                }
                return View(EmployeeVM);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(EmployeeVM);
            }

        }

    }
}
