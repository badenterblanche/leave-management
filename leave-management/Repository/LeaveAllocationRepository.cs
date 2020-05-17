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
        public async Task<bool> Create(LeaveAllocation par_locClass)
        {
            await _dbLeaveAllocationRepository.LeaveAllocations.AddAsync(par_locClass);
            return await Save();
        }

        public async Task<bool> Delete(LeaveAllocation par_locClass)
        {
            _dbLeaveAllocationRepository.LeaveAllocations.Remove(par_locClass);
            return await Save();
        }

        public async Task<ICollection<LeaveAllocation>> findAll()
        {
            return await _dbLeaveAllocationRepository.LeaveAllocations
                .Include(x => x.LeaveType)
                .Include(x => x.Employee)
                .ToListAsync();
        }

        public async Task<LeaveAllocation> FindByID(int par_locID)
        {
            return await _dbLeaveAllocationRepository.LeaveAllocations
                .Include(x => x.LeaveType)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.LeaveAllocationID == par_locID);
        }

        public async Task<bool> Save()
        {
            int intChanges = await _dbLeaveAllocationRepository.SaveChangesAsync();
            return intChanges > 0;
        }

        public async Task<bool> Update(LeaveAllocation par_locClass)
        {
            _dbLeaveAllocationRepository.LeaveAllocations.Update(par_locClass);
            return await Save();
        }

        public async Task<bool> checkExists(int par_ID)
        {
            return await _dbLeaveAllocationRepository.LeaveAllocations.AnyAsync(x => x.LeaveAllocationID == par_ID);
        }

        public async Task<bool> checkLeaveAllocated(string par_strEmployeeID, int par_intLeaveTypeID, int par_intPeriod)
        {
            par_intPeriod = DateTime.Now.Year;
            var varLeaveAllocation = await findAll();
            return varLeaveAllocation.Where(X => X.EmployeeID == par_strEmployeeID && X.LeaveTypeID == par_intLeaveTypeID && X.Period == par_intPeriod).Any();
        }

        public async Task<ICollection<LeaveAllocation>> getEmployeeLeaveAllocations(string par_strEmployeeID)
        {
            var varPeriod = DateTime.Now.Year;
            var varLeaveRequest = await findAll();
            return varLeaveRequest.Where(x => x.EmployeeID == par_strEmployeeID 
                                     && x.Period == varPeriod).ToList();
        }

        public async Task<LeaveAllocation> getEmployeeTypeAllocation(string par_strEmployeeID, int par_intLeaveTypeID)
        {
            var varPeriod = DateTime.Now.Year;
            var varLeaveAllocation = await findAll();
            return varLeaveAllocation.FirstOrDefault(x => x.EmployeeID == par_strEmployeeID && x.Period == varPeriod && x.LeaveTypeID == par_intLeaveTypeID);
        }
    }
}
