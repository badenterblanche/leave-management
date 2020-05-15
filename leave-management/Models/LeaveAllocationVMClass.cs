using leave_management.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveAllocationVMClass
    {
        [Key]
        public int LeaveAllocationID { get; set; }

        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }

        public EmployeeVMClass Employee { get; set; }
        public string EmployeeID { get; set; }

        public LeaveType LeaveType { get; set; }
        public int LeaveTypeID { get; set; }

        public int Period { get; set; }
    }

    public class CreateLeaveAllocationVMClass
    {
        [Key]
        public int NumberUpdated { get; set; }
        public List<LeaveTypeVMClass> ListLeaveTypeVMClass { get; set; }
    }

    public class EditLeaveAllocationVMClass
    {
        [Key]
        public int LeaveAllocationID { get; set; }
        public EmployeeVMClass Employee { get; set; }
        public int NumberOfDays { get; set; }
        public LeaveType LeaveType { get; set; }
        public string EmployeeID { get; set; }
    }

    public class ViewAllocationsVMClass
    {
        public EmployeeVMClass prop_clsEmployeeVMClass { get; set; }
        public string prop_strEmployeeID { get; set; }
        public List<LeaveAllocationVMClass> prop_lstLeaveAllocationVMClass { get; set; }


    }
}
