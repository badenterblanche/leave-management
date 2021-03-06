﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveTypesController : Controller
    {
        

        private readonly ILeaveTypeRepository _ILeaveTypeRepository;
        private readonly IMapper _IMapper;

        public LeaveTypesController(ILeaveTypeRepository par_ILeaveTypeRepository, IMapper par_IMapper)
        {
            _ILeaveTypeRepository = par_ILeaveTypeRepository;
            _IMapper = par_IMapper;
        }
        // GET: LeaveTypes
        
        public async Task<ActionResult> Index()
        {
            var leavetypes = await _ILeaveTypeRepository.findAll();

            var LeaveTypeModel = _IMapper.Map<List<LeaveType>, List<LeaveTypeVMClass>>(leavetypes.ToList());
            return View(LeaveTypeModel);
        }

        // GET: LeaveTypes/Details/5
        public async Task<ActionResult> Details(int id)
        {
            bool isExist = await _ILeaveTypeRepository.checkExists(id);
            if (!isExist)
            {
                return NotFound();
            }
            LeaveType locLeaveTypeClass = await _ILeaveTypeRepository.FindByID(id);
            var LeaveTypeModel = _IMapper.Map<LeaveTypeVMClass>(locLeaveTypeClass);

            return View(LeaveTypeModel);
        }

        // GET: LeaveTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LeaveTypeVMClass par_LeaveTypeVMClass)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(par_LeaveTypeVMClass);
                }

                var leavetype = _IMapper.Map<LeaveType>(par_LeaveTypeVMClass);
                leavetype.DateCreated = DateTime.Now;

                bool isSuccess = await _ILeaveTypeRepository.Create(leavetype);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(par_LeaveTypeVMClass);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveTypes/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (!await _ILeaveTypeRepository.checkExists(id)) 
            {
                return NotFound();
            }

            LeaveType loc_LeaveType = await _ILeaveTypeRepository.FindByID(id);
            var varModel = _IMapper.Map<LeaveTypeVMClass>(loc_LeaveType);

            

            return View(varModel);
        }

        // POST: LeaveTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeaveTypeVMClass par_locLeaveTypeVMClass)
        {
            try
            {
                // TODO: Add update logic here

                if (!ModelState.IsValid)
                {
                    return View(par_locLeaveTypeVMClass);
                }

                LeaveType locLeaveTypeDataClass = _IMapper.Map<LeaveType>(par_locLeaveTypeVMClass);
                locLeaveTypeDataClass.DateCreated = DateTime.Now;
                bool isSuccess = await _ILeaveTypeRepository.Update(locLeaveTypeDataClass);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(par_locLeaveTypeVMClass);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(par_locLeaveTypeVMClass);
            }
        }

        // GET: LeaveTypes/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            // TODO: Add delete logic here
            LeaveType locLeaveTypeRepositoryClass = await _ILeaveTypeRepository.FindByID(id);
            if (locLeaveTypeRepositoryClass == null)
            {
                return NotFound();
            }
            bool isSuccess = await _ILeaveTypeRepository.Delete(locLeaveTypeRepositoryClass);
            return RedirectToAction(nameof(Index));
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, LeaveTypeVMClass par_locLeaveTypeVMClass)
        {
            try
            {
                // TODO: Add delete logic here
                LeaveType locLeaveTypeRepositoryClass = await _ILeaveTypeRepository.FindByID(id);
                if (locLeaveTypeRepositoryClass == null)
                {
                    return NotFound();
                }
                bool isSuccess = await _ILeaveTypeRepository.Delete(locLeaveTypeRepositoryClass);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}