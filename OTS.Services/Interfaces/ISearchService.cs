using System.Collections.Generic;
using System.Threading.Tasks;

namespace OTS.Services.Interfaces
{
    public interface ISearchService
    {
        Task<Dictionary<string, dynamic>> GetAsync(string keywords);
    }
}
