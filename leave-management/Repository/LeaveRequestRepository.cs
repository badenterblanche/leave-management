using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _dbLeaveRequestRepository;
        public LeaveRequestRepository(ApplicationDbContext par_dbLeaveHistoryRepository)
        {
            _dbLeaveRequestRepository = par_dbLeaveHistoryRepository;
        }
        public bool Create(LeaveRequest par_locClass)
        {
            _dbLeaveRequestRepository.LeaveRequests.Add(par_locClass);
            return Save();
        }

        public bool Delete(LeaveRequest par_locClass)
        {
            _dbLeaveRequestRepository.LeaveRequests.Remove(par_locClass);
            return Save();
        }

        public ICollection<LeaveRequest> findAll()
        {
            return _dbLeaveRequestRepository.LeaveRequests
                .Include(x => x.RequestedEmployee)
                .Include(x => x.ApprovedByEmployee)
                .Include(x => x.LeaveType)
                .ToList();
        }

        public LeaveRequest FindByID(int par_locID)
        {
            return _dbLeaveRequestRepository.LeaveRequests
                .Include(x => x.RequestedEmployee)
                .Include(x => x.ApprovedByEmployee)
                .Include(x => x.LeaveType)
                .FirstOrDefault(x => x.LeaveRequestID == par_locID);
        }

        public bool Save()
        {
            int intChanges = _dbLeaveRequestRepository.SaveChanges();
            return intChanges > 0;
        }

        public bool Update(LeaveRequest par_locClass)
        {
            _dbLeaveRequestRepository.LeaveRequests.Update(par_locClass);
            return Save();
        }

        public bool checkExists(int par_ID)
        {
            return _dbLeaveRequestRepository.LeaveRequests.Any(x => x.LeaveRequestID == par_ID);
        }
    }
}
