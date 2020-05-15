﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using leave_management.Models;

namespace leave_management.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveHistory> LeaveHistories { get; set; }
        public DbSet<leave_management.Models.LeaveTypeVMClass> LeaveTypeVMClass { get; set; }
        public DbSet<leave_management.Models.CreateLeaveTypesVMClass> CreateLeaveTypesVMClass { get; set; }
        public DbSet<leave_management.Models.EmployeeVMClass> EmployeeVMClass { get; set; }
        public DbSet<leave_management.Models.LeaveAllocationVMClass> LeaveAllocationVMClass { get; set; }
        public DbSet<leave_management.Models.EditLeaveAllocationVMClass> EditLeaveAllocationVMClass { get; set; }
        public DbSet<leave_management.Models.CreateLeaveAllocationVMClass> CreateLeaveAllocationVMClass { get; set; }
    }
}
