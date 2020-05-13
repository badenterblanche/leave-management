using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        bool checkLeaveAllocated(string par_strEmployeeID, int par_intLeaveTypeID, int par_intPeriod);
    }
}