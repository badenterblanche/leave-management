using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _dbLeaveAllocationRepository.LeaveAllocations.ToList();
        }

        public LeaveAllocation FindByID(int par_locID)
        {
            return _dbLeaveAllocationRepository.LeaveAllocations.Find(par_locID);
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
    }
}
