using CSVTrello.Domain.Models;

namespace CSVTrello.Domain.Repositories
{
    public interface ITenderRepository
    {
        Task<IEnumerable<Tender>> ListAsync();
        Task AddAsync(Tender tender);
        Task<Tender> FindByIdAsync(int id);
        void Update(Tender tender);
    }
}
