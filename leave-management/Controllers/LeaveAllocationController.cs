using System;
using System.Collections.Generic;
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
        private readonly ILeaveAllocationRepository _ILeaveApplicationRepository;
        private readonly IMapper _IMapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveAllocationController(ILeaveTypeRepository par_ILeaveTypeRepository, 
            ILeaveAllocationRepository par_ILeaveApplicationRepository, IMapper par_IMapper,
            UserManager<Employee> par_UserManager)
        {
            _ILeaveTypeRepository = par_ILeaveTypeRepository;
            _ILeaveApplicationRepository = par_ILeaveApplicationRepository;
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
                if (!_ILeaveApplicationRepository.checkLeaveAllocated(forEmployee.Id, id, DateTime.Now.Year))
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
                    _ILeaveApplicationRepository.Create(varLeaveAllocationMap);
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaveAllocation/Create
        public ActionResult Create()
        {
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
            return View();
            //test again
        }

        // POST: LeaveAllocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
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