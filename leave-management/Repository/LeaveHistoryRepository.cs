using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {
        private readonly ApplicationDbContext _dbLeaveHistoryRepository;
        public LeaveHistoryRepository(ApplicationDbContext par_dbLeaveHistoryRepository)
        {
            _dbLeaveHistoryRepository = par_dbLeaveHistoryRepository;
        }
        public bool Create(LeaveHistory par_locClass)
        {
            _dbLeaveHistoryRepository.LeaveHistories.Add(par_locClass);
            return Save();
        }

        public bool Delete(LeaveHistory par_locClass)
        {
            _dbLeaveHistoryRepository.LeaveHistories.Remove(par_locClass);
            return Save();
        }

        public ICollection<LeaveHistory> findAll()
        {
            return _dbLeaveHistoryRepository.LeaveHistories.ToList();
        }

        public LeaveHistory FindByID(int par_locID)
        {
            return _dbLeaveHistoryRepository.LeaveHistories.Find(par_locID);
        }

        public bool Save()
        {
            int intChanges = _dbLeaveHistoryRepository.SaveChanges();
            return intChanges > 0;
        }

        public bool Update(LeaveHistory par_locClass)
        {
            _dbLeaveHistoryRepository.LeaveHistories.Update(par_locClass);
            return Save();
        }
    }
}
