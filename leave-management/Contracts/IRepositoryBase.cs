using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface IRepositoryBase<parClass> where parClass : class
    {
        ICollection<parClass> findAll();
        parClass FindByID(int par_locID);
        bool Create(parClass par_locClass);
        bool Update(parClass par_locClass);
        bool Delete(parClass par_locClass);
        bool Save();
    }
}
