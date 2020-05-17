using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;

namespace leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _dbLeaveRequestRepository;
        public LeaveRequestRepository(ApplicationDbContext par_dbLeaveHistoryRepository)
        {
            _dbLeaveRequestRepository = par_dbLeaveHistoryRepository;
        }
        public async Task<bool> Create(LeaveRequest par_locClass)
        {
            await _dbLeaveRequestRepository.LeaveRequests.AddAsync(par_locClass);
            return await Save();
        }

        public async Task<bool> Delete(LeaveRequest par_locClass)
        {
            _dbLeaveRequestRepository.LeaveRequests.Remove(par_locClass);
            return await Save();
        }

        public async Task<ICollection<LeaveRequest>> findAll()
        {
            return await _dbLeaveRequestRepository.LeaveRequests
                .Include(x => x.RequestedEmployee)
                .Include(x => x.ApprovedByEmployee)
                .Include(x => x.LeaveType)
                .ToListAsync();
        }

        public async Task<LeaveRequest> FindByID(int par_locID)
        {
            return await _dbLeaveRequestRepository.LeaveRequests
                .Include(x => x.RequestedEmployee)
                .Include(x => x.ApprovedByEmployee)
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.LeaveRequestID == par_locID);
        }

        public async Task<bool> Save()
        {
            int intChanges = await _dbLeaveRequestRepository.SaveChangesAsync();
            return intChanges > 0;
        }

        public async Task<bool> Update(LeaveRequest par_locClass)
        {
            _dbLeaveRequestRepository.LeaveRequests.Update(par_locClass);
            return await Save();
        }

        public async Task<bool> checkExists(int par_ID)
        {
            return await _dbLeaveRequestRepository.LeaveRequests.AnyAsync(x => x.LeaveRequestID == par_ID);
        }
    }
}
