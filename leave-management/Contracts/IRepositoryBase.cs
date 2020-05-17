using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface IRepositoryBase<parClass> where parClass : class
    {
        Task<ICollection<parClass>> findAll();
        Task<parClass> FindByID(int par_locID);
        Task<bool> Create(parClass par_locClass);
        Task<bool> Update(parClass par_locClass);
        Task<bool> Delete(parClass par_locClass);
        Task<bool> Save();
        Task<bool> checkExists(int par_ID);
    }
}
