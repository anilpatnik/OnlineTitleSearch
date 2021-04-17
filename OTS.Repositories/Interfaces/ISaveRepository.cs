using System.Threading.Tasks;

namespace OTS.Repositories.Interfaces
{
    public interface ISaveRepository
    {
        Task CompleteAsync();
    }
}
