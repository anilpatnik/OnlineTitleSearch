using System.Collections.Generic;
using System.Threading.Tasks;
using OTS.Models;
using OTS.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using OTS.Repositories.Interfaces;

namespace OTS.Repositories
{
    public class LookupRepository : BaseRepository, ILookupRepository
    {
        public LookupRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Lookup>> ListAsync()
        {
            return await _context.Lookups.AsNoTracking().ToListAsync();
        }

        public async Task<Lookup> FindByIdAsync(int id)
        {
            return await _context.Lookups.FindAsync(id);
        }

        public void Add(Lookup payload)
        {
            _context.Lookups.Add(payload);
        }

        public void Update(Lookup payload)
        {
            _context.Lookups.Update(payload);
        }

        public void Remove(Lookup payload)
        {
            _context.Lookups.Remove(payload);
        }
    }
}
