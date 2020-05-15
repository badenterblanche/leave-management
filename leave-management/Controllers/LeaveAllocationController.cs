using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveTypeRepository _ILeaveTypeRepository;
        private readonly ILeaveAllocationRepository _ILeaveAllocationRepository;
        private readonly IMapper _IMapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveAllocationController(
            ILeaveTypeRepository par_ILeaveTypeRepository, 
            ILeaveAllocationRepository par_ILeaveAllocationRepository, 
            IMapper par_IMapper,
            UserManager<Employee> par_UserManager)
        {
            _ILeaveTypeRepository = par_ILeaveTypeRepository;
            _ILeaveAllocationRepository = par_ILeaveAllocationRepository;
            _IMapper = par_IMapper;
            _userManager = par_UserManager;
        }
        // GET: LeaveAllocation
        public ActionResult Index()
        {
            var leavetypes = _ILeaveTypeRepository.findAll().ToList();
            var ListLeaveTypes = _IMapper.Map<List<LeaveType>, List<LeaveTypeVMClass>>(leavetypes);

            var varListLeaveTypesMode = new CreateLeaveAllocationVMClass
            {
                ListLeaveTypeVMClass = ListLeaveTypes,
                NumberUpdated = 0
            };
            return View(varListLeaveTypesMode);
        }

        public ActionResult SetLeave (int id)
        {
            var varLeaveType = _ILeaveTypeRepository.FindByID(id);
            var varEmployees = _userManager.GetUsersInRoleAsync("Employee").Result;

            int intCounter = 0;
            foreach (var forEmployee in varEmployees)
            {
                if (!_ILeaveAllocationRepository.checkLeaveAllocated(forEmployee.Id, id, DateTime.Now.Year))
                {
                    intCounter++;
                    LeaveAllocationVMClass locLeaveAllocationVMClass = new LeaveAllocationVMClass
                    {
                        NumberOfDays = varLeaveType.DefaultDays,
                        DateCreated = DateTime.Now,
                        EmployeeID = forEmployee.Id,
                        LeaveTypeID = id,
                        Period = DateTime.Now.Year
                    };

                    var varLeaveAllocationMap = _IMapper.Map<LeaveAllocation>(locLeaveAllocationVMClass);
                    _ILeaveAllocationRepository.Create(varLeaveAllocationMap);
                }

            }

            CreateLeaveAllocationVMClass locCreateLeaveAllocationVMClass = new CreateLeaveAllocationVMClass
            {
                NumberUpdated = intCounter
            };

            return View(locCreateLeaveAllocationVMClass);
        }

        public ActionResult EmployeeList()
        {
            var lstEmployees = _userManager.GetUsersInRoleAsync("Employee").Result;
            var varEmployeesModel = _IMapper.Map<List<EmployeeVMClass>>(lstEmployees);

            return View(varEmployeesModel);
        }


        // GET: LeaveAllocation/Details/5
        public ActionResult Details(string id)
        {
            var varEmployeeVM = _IMapper.Map<EmployeeVMClass>(_userManager.FindByIdAsync(id).Result);

            var Period = DateTime.Now.Year;

            var varEmployeeLeaveAllocationsVM = _IMapper.Map <List<LeaveAllocationVMClass>>(_ILeaveAllocationRepository.getEmployeeLeaveAllocations(id));

            ViewAllocationsVMClass locViewAllocationsVMClass = new ViewAllocationsVMClass
            {
                prop_clsEmployeeVMClass = varEmployeeVM,
                prop_lstLeaveAllocationVMClass = varEmployeeLeaveAllocationsVM
            };

            return View(locViewAllocationsVMClass);
        }

        // GET: LeaveAllocation/Create
        public ActionResult Create()
        {//11122224444555
            return View();
        }

        // POST: LeaveAllocation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocation/Edit/5
        public ActionResult Edit(int id)
        {
            var varLeaveAllocation_VMClass = _IMapper.Map<EditLeaveAllocationVMClass>(_ILeaveAllocationRepository.FindByID(id));
            return View(varLeaveAllocation_VMClass);
        }

        // POST: LeaveAllocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLeaveAllocationVMClass par_EditLeaveAllocationVMClass)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(par_EditLeaveAllocationVMClass);
                }

                var varRecord = _ILeaveAllocationRepository.FindByID(par_EditLeaveAllocationVMClass.LeaveAllocationID);
                varRecord.NumberOfDays = par_EditLeaveAllocationVMClass.NumberOfDays;

                bool isSuccess = _ILeaveAllocationRepository.Update(varRecord);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error with update");
                    return View(par_EditLeaveAllocationVMClass);
                }
                // TODO: Add update logic here

                return RedirectToAction(nameof(Details), new { id = par_EditLeaveAllocationVMClass.EmployeeID });
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}