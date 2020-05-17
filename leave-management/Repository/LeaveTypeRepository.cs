using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _dbLeaveTypeRepository;
        public LeaveTypeRepository(ApplicationDbContext par_dbLeaveTypeRepository)
        {
            _dbLeaveTypeRepository = par_dbLeaveTypeRepository;
        }

       

        public async Task<bool> Create(LeaveType par_locClass)
        {
            await _dbLeaveTypeRepository.LeaveTypes.AddAsync(par_locClass);
            return await Save();
        }

        public async Task<bool> Delete(LeaveType par_locClass)
        {
            _dbLeaveTypeRepository.LeaveTypes.Remove(par_locClass);
            return await Save();
        }

        public async Task<ICollection<LeaveType>> findAll()
        {
            return await _dbLeaveTypeRepository.LeaveTypes.ToListAsync();
        }

        public async Task<LeaveType> FindByID(int par_locID)
        {
            return await _dbLeaveTypeRepository.LeaveTypes.FindAsync(par_locID);
        }

        public ICollection<LeaveType> getEmployeesByLeaveType(int LeaveTypeID)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Save()
        {
            int intChanges = await _dbLeaveTypeRepository.SaveChangesAsync();
            return intChanges > 0;
        }

        public async Task<bool> Update(LeaveType par_locClass)
        {
            _dbLeaveTypeRepository.LeaveTypes.Update(par_locClass);
            return await Save();
        }

        public async Task<bool> checkExists(int par_ID)
        {
            return await _dbLeaveTypeRepository.LeaveTypes.AnyAsync(x => x.LeaveTypeID == par_ID);
        }
    }

}
