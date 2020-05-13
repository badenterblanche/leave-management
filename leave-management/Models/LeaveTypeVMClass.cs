using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveTypeVMClass
    {
        [Key]
        public int LeaveTypeID { get; set; }
        [Required]      
        [Display(Name="Leave Type")]
        public string Name { get; set; }
        [Display(Name="Date Created")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateCreated { get; set; }
        [Required]
        [Range(1,25, ErrorMessage = "Invalid Range")]
        [Display(Name = "Default Number of days")]
        public int DefaultDays { get; set; }
    }

    public class CreateLeaveTypesVMClass
    {
        [Key]
        public int LeaveTypeID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
