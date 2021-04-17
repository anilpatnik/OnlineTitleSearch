using System.Collections.Generic;
using System.Threading.Tasks;
using OTS.Models;
using OTS.Services.Common;

namespace OTS.Services.Interfaces
{
    public interface ILookupService
    {
        Task<IEnumerable<Lookup>> ListAsync();

        Task<Response<Lookup>> FindByIdAsync(int id);

        Task<Response<Lookup>> SaveAsync(Lookup payload);
        
        Task<Response<Lookup>> UpdateAsync(int id, Lookup payload);
        
        Task<Response<Lookup>> DeleteAsync(int id);
    }
}
