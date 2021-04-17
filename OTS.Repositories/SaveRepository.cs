using System.Threading.Tasks;
using OTS.Repositories.Contexts;
using OTS.Repositories.Interfaces;

namespace OTS.Repositories
{
    public class SaveRepository : BaseRepository, ISaveRepository
    {        
        public SaveRepository(AppDbContext context) : base(context) { }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }        
    }
}
