using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveRequestVMClass
    {
        public int LeaveRequestID { get; set; }
        public EmployeeVMClass RequestedEmployee { get; set; }
        public string RequestedEmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveTypeVMClass LeaveType { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        public EmployeeVMClass ApprovedByEmployee { get; set; }
        public string ApprovedByEmployeeID { get; set; }
    }

    public class LeaveRequestViewAdminVM
    {
        public int intTotalRequests { get; set; }
        public int intTotalApprovedRequests { get; set; }
        public int intTotalPendingRequests { get; set; }
        public int intTotalRejectedRequests { get; set; }
        public List<LeaveRequestVMClass> lstLeaveRequestVMClass { get; set; }
    }

    public class CreateLeaveRequestVMClass
    {
        public int LeaveRequestID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public LeaveTypeVMClass LeaveType { get; set; }
        public IEnumerable<SelectListItem> selectLeaveTypes { get; set; }
        public int LeaveTypeID { get; set; }
        public string RequestedByEmployeeId { get; set; }
        public DateTime DateRequested { get; set; }
    }
}
