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
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult> Index()
        {
            var leavetypes = await _ILeaveTypeRepository.findAll();
            var ListLeaveTypes = _IMapper.Map<List<LeaveType>, List<LeaveTypeVMClass>>(leavetypes.ToList());

            var varListLeaveTypesMode = new CreateLeaveAllocationVMClass
            {
                ListLeaveTypeVMClass = ListLeaveTypes,
                NumberUpdated = 0
            };
            return View(varListLeaveTypesMode);
        }

        public async Task<ActionResult> SetLeave (int id)
        {
            var varLeaveType = await _ILeaveTypeRepository.FindByID(id);
            var varEmployees = await _userManager.GetUsersInRoleAsync("Employee");

            int intCounter = 0;
            foreach (var forEmployee in varEmployees)
            {
                if (!await _ILeaveAllocationRepository.checkLeaveAllocated(forEmployee.Id, id, DateTime.Now.Year))
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
                    await _ILeaveAllocationRepository.Create(varLeaveAllocationMap);
                }

            }

            CreateLeaveAllocationVMClass locCreateLeaveAllocationVMClass = new CreateLeaveAllocationVMClass
            {
                NumberUpdated = intCounter
            };

            return View(locCreateLeaveAllocationVMClass);
        }

        public async Task<ActionResult> EmployeeList()
        {
            var lstEmployees = await _userManager.GetUsersInRoleAsync("Employee");
            var varEmployeesModel = _IMapper.Map<List<EmployeeVMClass>>(lstEmployees);

            return View(varEmployeesModel);
        }


        // GET: LeaveAllocation/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var varEmployeeVM = _IMapper.Map<EmployeeVMClass>(await _userManager.FindByIdAsync(id));

            var Period = DateTime.Now.Year;

            var varEmployeeLeaveAllocationsVM = _IMapper.Map <List<LeaveAllocationVMClass>>(await _ILeaveAllocationRepository.getEmployeeLeaveAllocations(id));

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
        public async Task<ActionResult> Edit(int id)
        {
            var varLeaveAllocation_VMClass = _IMapper.Map<EditLeaveAllocationVMClass>(await _ILeaveAllocationRepository.FindByID(id));
            return View(varLeaveAllocation_VMClass);
        }

        // POST: LeaveAllocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditLeaveAllocationVMClass par_EditLeaveAllocationVMClass)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(par_EditLeaveAllocationVMClass);
                }

                var varRecord = await _ILeaveAllocationRepository.FindByID(par_EditLeaveAllocationVMClass.LeaveAllocationID);
                varRecord.NumberOfDays = par_EditLeaveAllocationVMClass.NumberOfDays;

                bool isSuccess = await _ILeaveAllocationRepository.Update(varRecord);
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