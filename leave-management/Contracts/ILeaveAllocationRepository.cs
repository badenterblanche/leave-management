using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace leave_management.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        Task<bool> checkLeaveAllocated(string par_strEmployeeID, int par_intLeaveTypeID, int par_intPeriod);
        Task<ICollection<LeaveAllocation>> getEmployeeLeaveAllocations(string par_strEmployeeID);
        Task<LeaveAllocation> getEmployeeTypeAllocation(string par_strEmployeeID, int par_intLeaveTypeID);
    }
}