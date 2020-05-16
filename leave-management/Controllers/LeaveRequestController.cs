using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace leave_management.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveTypeRepository _ILeaveTypeRepository;
        private readonly ILeaveRequestRepository _ILeaveRequestRepository;
        private readonly ILeaveAllocationRepository _ILeaveAllocationRepository;
        private readonly IMapper _IMapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveRequestController(
            ILeaveTypeRepository par_ILeaveTypeRepository,
            ILeaveRequestRepository par_ILeaveRequestRepository,
            ILeaveAllocationRepository par_ILeaveAllocationRepository,
            IMapper par_IMapper,
            UserManager< Employee > par_UserManager)
        {
            _ILeaveTypeRepository = par_ILeaveTypeRepository;
            _ILeaveRequestRepository = par_ILeaveRequestRepository;
            _ILeaveAllocationRepository = par_ILeaveAllocationRepository;
            _IMapper = par_IMapper;
            _userManager = par_UserManager;
        }

        [Authorize(Roles = "Administrator")]
        // GET: LeaveRequest
        public ActionResult Index()
        {
            var varLeaveRequestRepoClass = _ILeaveRequestRepository.findAll().OrderByDescending(x => x.DateRequested);
            var var_lstLeaveRequestVMClass = _IMapper.Map<List<LeaveRequestVMClass>>(varLeaveRequestRepoClass);

            var varLeaveRequestViewAdminVM = new LeaveRequestViewAdminVM
            {
                intTotalRequests = var_lstLeaveRequestVMClass.Count,
                intTotalApprovedRequests = var_lstLeaveRequestVMClass.Count(x => x.Approved == true),
                intTotalRejectedRequests = var_lstLeaveRequestVMClass.Count(x => x.Approved == false),
                intTotalPendingRequests = var_lstLeaveRequestVMClass.Count(x => x.Approved == null),
                lstLeaveRequestVMClass = var_lstLeaveRequestVMClass
            };

            return View(varLeaveRequestViewAdminVM);
        }

        // GET: LeaveRequest/Details/5
        public ActionResult Details(int id)
        {
            var varLeaveRequestRepositoryClass = _ILeaveRequestRepository.FindByID(id);

            var varLeaveRequestVMClass = _IMapper.Map<LeaveRequestVMClass>(varLeaveRequestRepositoryClass);

            return View(varLeaveRequestVMClass);
        }

        // GET: LeaveRequest/Create
        public ActionResult Create()
        {
            var varLeaveTypeRepositoryClass = _ILeaveTypeRepository.findAll();

            var varSelectListItem = varLeaveTypeRepositoryClass.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.LeaveTypeID.ToString()
            });

            var varLeaveTypesModel = new CreateLeaveRequestVMClass
            {
                selectLeaveTypes = varSelectListItem
            };
            var varLeaveTypeRepositoryClassList = _IMapper.Map<List<LeaveTypeVMClass>>(varLeaveTypeRepositoryClass);
            return View(varLeaveTypesModel);
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLeaveRequestVMClass parCreateLeaveRequestVMClass)
        {
            try
            {
                var varStartDate = Convert.ToDateTime(parCreateLeaveRequestVMClass.StartDate);
                var varEndDate = Convert.ToDateTime(parCreateLeaveRequestVMClass.EndDate);

                var varLeaveTypeRepositoryClass = _ILeaveTypeRepository.findAll();
                var varSelectListItem = varLeaveTypeRepositoryClass.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.LeaveTypeID.ToString()
                });

                parCreateLeaveRequestVMClass.selectLeaveTypes = varSelectListItem;

                if (!ModelState.IsValid)
                {
                    return View(parCreateLeaveRequestVMClass);
                }


                int intWTF = (int)DateTime.Compare(varEndDate, varStartDate);
                if ((int)DateTime.Compare(varEndDate, varStartDate) < 0)
                {
                    ModelState.AddModelError("", "To Date < then From Date");
                    return View(parCreateLeaveRequestVMClass);
                }

                

                var varEmployeeLoggedIn = _userManager.GetUserAsync(User).Result;
                var varLeaveAllocationClass = _ILeaveAllocationRepository.getEmployeeTypeAllocation(varEmployeeLoggedIn.Id, parCreateLeaveRequestVMClass.LeaveTypeID);

                if (varLeaveAllocationClass.LeaveAllocationID == 0)
                {
                    ModelState.AddModelError("", "No Leave Allocated");
                    return View(parCreateLeaveRequestVMClass);
                }

                double dblDaysRequested = (double)(varEndDate.Date - varStartDate.Date).TotalDays;
                if (dblDaysRequested > varLeaveAllocationClass.NumberOfDays)
                {
                    ModelState.AddModelError("", "Exceeds Leave Allocated");
                    return View(parCreateLeaveRequestVMClass);
                }

                var varLeaveRequestVMClass = new LeaveRequestVMClass
                {
                    StartDate           =   varStartDate,
                    EndDate             =   varEndDate,
                    LeaveTypeID         =   parCreateLeaveRequestVMClass.LeaveTypeID,
                    DateRequested       =   DateTime.Now,
                    DateActioned        =   DateTime.Now,
                    RequestedEmployeeID =   varEmployeeLoggedIn.Id
                };
                
                var varLeaveRequestDataModel = _IMapper.Map<LeaveRequest>(varLeaveRequestVMClass);
                bool isSuccess = _ILeaveRequestRepository.Create(varLeaveRequestDataModel);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Insert Applivation Error");
                    return View(parCreateLeaveRequestVMClass);
                }


                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception exError)
            {
                ModelState.AddModelError("", exError.Message);
                return View(parCreateLeaveRequestVMClass);
            }
        }

        // GET: LeaveRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Edit/5
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

        // GET: LeaveRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Delete/5
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

        public ActionResult ApproveRequest (int id)
        {

            try
            {
                var varLeaveRequestRepositoryClass = _ILeaveRequestRepository.FindByID(id);
                varLeaveRequestRepositoryClass.Approved = true;
                varLeaveRequestRepositoryClass.DateActioned = DateTime.Now;
                var varEmployeeLoggedIn = _userManager.GetUserAsync(User).Result;
                varLeaveRequestRepositoryClass.ApprovedByEmployeeID = varEmployeeLoggedIn.Id;

                bool isSuccess = _ILeaveRequestRepository.Update(varLeaveRequestRepositoryClass);

                var varLeaveAllocationRepositoryClass = _ILeaveAllocationRepository.getEmployeeTypeAllocation(varLeaveRequestRepositoryClass.RequestedEmployeeID, varLeaveRequestRepositoryClass.LeaveTypeID);

                double dblDays = (varLeaveRequestRepositoryClass.EndDate.Date - varLeaveRequestRepositoryClass.StartDate.Date).TotalDays;
                double dblBalance = varLeaveAllocationRepositoryClass.NumberOfDays - dblDays;

                varLeaveAllocationRepositoryClass.NumberOfDays = (int)dblBalance;

                bool isSuccessAllocation = _ILeaveAllocationRepository.Update(varLeaveAllocationRepositoryClass);

                return RedirectToAction(nameof(Index), "Home");
            }
            catch
            {
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        public ActionResult RejectRequest(int id)
        {
            try
            {
                var varLeaveRequestRepositoryClass = _ILeaveRequestRepository.FindByID(id);
                varLeaveRequestRepositoryClass.Approved = false;
                varLeaveRequestRepositoryClass.DateActioned = DateTime.Now;
                var varEmployeeLoggedIn = _userManager.GetUserAsync(User).Result;
                varLeaveRequestRepositoryClass.ApprovedByEmployeeID = varEmployeeLoggedIn.Id;

                bool isSuccess = _ILeaveRequestRepository.Update(varLeaveRequestRepositoryClass);
                return RedirectToAction(nameof(Index), "Home");
            }
            catch 
            {
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}