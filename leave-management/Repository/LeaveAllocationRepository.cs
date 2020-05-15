using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _dbLeaveAllocationRepository;
        public LeaveAllocationRepository(ApplicationDbContext par_dbLeaveAllocationRepository)
        {
            _dbLeaveAllocationRepository = par_dbLeaveAllocationRepository;
        }
        public bool Create(LeaveAllocation par_locClass)
        {
            _dbLeaveAllocationRepository.LeaveAllocations.Add(par_locClass);
            return Save();
        }

        public bool Delete(LeaveAllocation par_locClass)
        {
            _dbLeaveAllocationRepository.LeaveAllocations.Remove(par_locClass);
            return Save();
        }

        public ICollection<LeaveAllocation> findAll()
        {
            return _dbLeaveAllocationRepository.LeaveAllocations
                .Include(x => x.LeaveType)
                .Include(x => x.Employee)
                .ToList();
        }

        public LeaveAllocation FindByID(int par_locID)
        {
            return _dbLeaveAllocationRepository.LeaveAllocations
                .Include(x => x.LeaveType)
                .Include(x => x.Employee)
                .FirstOrDefault(x => x.LeaveAllocationID == par_locID);
        }

        public bool Save()
        {
            int intChanges = _dbLeaveAllocationRepository.SaveChanges();
            return intChanges > 0;
        }

        public bool Update(LeaveAllocation par_locClass)
        {
            _dbLeaveAllocationRepository.LeaveAllocations.Update(par_locClass);
            return Save();
        }

        public bool checkExists(int par_ID)
        {
            return _dbLeaveAllocationRepository.LeaveAllocations.Any(x => x.LeaveAllocationID == par_ID);
        }

        public bool checkLeaveAllocated(string par_strEmployeeID, int par_intLeaveTypeID, int par_intPeriod)
        {
            par_intPeriod = DateTime.Now.Year;

             return findAll().Where(X => X.EmployeeID == par_strEmployeeID && X.LeaveTypeID == par_intLeaveTypeID && X.Period == par_intPeriod).Any();
        }

        public ICollection<LeaveAllocation> getEmployeeLeaveAllocations(string par_strEmployeeID)
        {
            var varPeriod = DateTime.Now.Year;
            return findAll()
                .Where(x => x.EmployeeID == par_strEmployeeID && x.Period == varPeriod)
                .ToList();
        }
    }
}
