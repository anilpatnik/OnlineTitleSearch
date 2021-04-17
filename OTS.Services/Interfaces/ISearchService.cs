using System.Collections.Generic;
using System.Threading.Tasks;

namespace OTS.Services.Interfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<dynamic>> GetAsync(string keywords);
    }
}
