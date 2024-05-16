using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CSVTrello.Domain.Models;
using CSVTrello.Domain.Repositories;
using CSVTrello.Domain.Services;
using CSVTrello.DTOs;
using CSVTrello.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CSVTrello.Services
{
    public class TenderService: ITenderService
    {
        private readonly ITenderRepository _tenderRepository;
        private readonly ITrelloService _trelloService;
        private readonly ILogger<TenderService> _logger;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;


        public TenderService(ITenderRepository tenderRepository, ITrelloService trelloService, ILogger<TenderService> logger, AppDbContext context, IConfiguration configuration)
        {
            _tenderRepository = tenderRepository;
            _trelloService = trelloService;
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public async Task ProcessCsvFileAsync(IFormFile file)
        {
            _logger.LogInformation($"Processing CSV file '{file.FileName}'.");

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<TenderMap>();
                var records = csv.GetRecordsAsync<Tender>();

                await foreach (var record in records)
                {
                    if (record == null)
                    {
                        _logger.LogError("Encountered a null record in the CSV file.");
                        continue;
                    }
                    
                    try
                    {
                        _logger.LogInformation($"Processing tender '{record.TenderId}'.");
                        await _tenderRepository.AddAsync(record);
                        _logger.LogInformation($"Added tender '{record.TenderId}' to the repository.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error processing record '{record.TenderId}': {ex.Message}");
                    }
                }
            }
        }
        public async Task UpdateTrelloAsync()
        {
            _logger.LogInformation("Updating Trello.");

            var tenders = await _tenderRepository.ListAsync();

            var apiKey = _configuration["Trello:ApiKey"];
            var apiToken = _configuration["Trello:ApiToken"];
            var boardId = _configuration["Trello:BoardId"];
            var listId = _configuration["Trello:ListId"];


            await _trelloService.AuthenticateAsync(apiKey, apiToken);

            foreach (var tender in tenders)
            {
                var uniqueId = $"{tender.TenderId}-{tender.LotNumber}";

                var card = await _trelloService.GetCardAsync(boardId, uniqueId);

                if (card == null)
                {
                    card = await _trelloService.CreateCardAsync(listId, uniqueId);
                    _logger.LogInformation($"Created new card '{uniqueId}'.");
                }

                var description = $"Name: {tender.Name}\n" +
                                  $"Tender Name: {tender.TenderName}\n" +
                                  $"Deadline: {tender.Deadline}\n" +
                                  $"Expiration Date: {tender.ExpirationDate}\n" +
                                  $"Has Documents: {tender.HasDocuments}\n" +
                                  $"Location: {tender.Location}\n" +
                                  $"Publication Date: {tender.PublicationDate}\n" +
                                  $"Status: {tender.Status}\n" +
                                  $"Currency: {tender.Currency}\n" +
                                  $"Value: {tender.Value}";
                await _trelloService.UpdateCardDescriptionAsync(card, description);
                _logger.LogInformation($"Updated description for card '{uniqueId}'.");

                await _trelloService.MoveCardToListAsync(card, tender.Status.ToString());
                _logger.LogInformation($"Moved card '{uniqueId}' to list '{tender.Status}'.");
            }

            _logger.LogInformation("Finished updating Trello.");
        }

        public async Task<List<Tender>> GetAllTendersAsync()
        {
            return await _context.Tenders.ToListAsync();
        }
    }
}