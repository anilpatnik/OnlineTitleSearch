using System.Collections.Generic;
using System.Threading.Tasks;
using OTS.Models;

namespace OTS.Repositories.Interfaces
{
    public interface ILookupRepository
    {
        Task<IEnumerable<Lookup>> ListAsync();

        Task<Lookup> FindByIdAsync(int id);

        void Add(Lookup payload);
        
        void Update(Lookup payload);
        
        void Remove(Lookup payload);
    }
}
