using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _dbLeaveTypeRepository;
        public LeaveTypeRepository(ApplicationDbContext par_dbLeaveTypeRepository)
        {
            _dbLeaveTypeRepository = par_dbLeaveTypeRepository;
        }

       

        public bool Create(LeaveType par_locClass)
        {
            _dbLeaveTypeRepository.LeaveTypes.Add(par_locClass);
            return Save();
        }

        public bool Delete(LeaveType par_locClass)
        {
            _dbLeaveTypeRepository.LeaveTypes.Remove(par_locClass);
            return Save();
        }

        public ICollection<LeaveType> findAll()
        {
            return _dbLeaveTypeRepository.LeaveTypes.ToList();
        }

        public LeaveType FindByID(int par_locID)
        {
            return _dbLeaveTypeRepository.LeaveTypes.Find(par_locID);
        }

        public ICollection<LeaveType> getEmployeesByLeaveType(int LeaveTypeID)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            int intChanges = _dbLeaveTypeRepository.SaveChanges();
            return intChanges > 0;
        }

        public bool Update(LeaveType par_locClass)
        {
            _dbLeaveTypeRepository.LeaveTypes.Update(par_locClass);
            return Save();
        }

        public bool checkExists(int par_ID)
        {
            return _dbLeaveTypeRepository.LeaveTypes.Any(x => x.LeaveTypeID == par_ID);
        }
    }

}
