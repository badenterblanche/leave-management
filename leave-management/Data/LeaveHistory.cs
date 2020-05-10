using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class LeaveHistory
    {
        [Key]
        public int LeaveHistoryID { get; set; }
        [ForeignKey("RequestedEmployeeID")]
        public Employee RequestedEmployee { get; set; }
        public string RequestedEmployeeID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [ForeignKey("LeaveTypeID")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        [ForeignKey("ApprovedByEmployeeID")]
        public Employee ApprovedByEmployee { get; set; }
        public string ApprovedByEmployeeID { get; set; }
    }
}
