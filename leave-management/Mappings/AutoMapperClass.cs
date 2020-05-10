using AutoMapper;
using leave_management.Data;
using leave_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Mappings
{
    public class AutoMapperClass : Profile
    {
        public AutoMapperClass()
        {
            CreateMap<LeaveType, LeaveTypeVMClass>().ReverseMap();
            CreateMap<LeaveHistory, LeaveHistoryVMClass>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationVMClass>().ReverseMap();
            CreateMap<Employee, EmployeeVMClass>().ReverseMap();
        }
    }
}
