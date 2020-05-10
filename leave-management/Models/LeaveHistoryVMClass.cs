using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveHistoryVMClass
    {
        public int LeaveHistoryID { get; set; }
        public EmployeeVMClass RequestedEmployee { get; set; }
        public string RequestedEmployeeID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int LeaveTypeID { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        public EmployeeVMClass ApprovedByEmployee { get; set; }
        public string ApprovedByEmployeeID { get; set; }
    }
}
