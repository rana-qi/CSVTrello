using CSVTrello.Domain.Models;

namespace CSVTrello.Domain.Services
{
    public interface ITenderService
    {
        Task ProcessCsvFileAsync(IFormFile file);
        Task UpdateTrelloAsync();
        Task<List<Tender>> GetAllTendersAsync();
    }
}
