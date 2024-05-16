using CSVTrello.Domain.Models;
using CSVTrello.Domain.Repositories;
using CSVTrello.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CSVTrello.Persistence.Repositories
{
    public class TenderRepository: BaseRepository, ITenderRepository
    {
        public TenderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tender>> ListAsync()
        {
            return await _context.Tenders.ToListAsync();
        }

        public async Task AddAsync(Tender tender)
        {
            await _context.Tenders.AddAsync(tender);
            await _context.SaveChangesAsync();
        }

        public async Task<Tender> FindByIdAsync(int id)
        {
            return await _context.Tenders.FindAsync(id);
        }

        public void Update(Tender tender)
        {
            _context.Tenders.Update(tender);
            _context.SaveChanges();
        }
    }
}
