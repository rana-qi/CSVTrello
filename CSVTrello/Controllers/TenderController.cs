using CSVTrello.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSVTrello.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController: Controller
    {
        private readonly ITenderService _tenderService;

        public TenderController(ITenderService tenderService)
        {
            _tenderService = tenderService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            await _tenderService.ProcessCsvFileAsync(file);

            return Ok("File uploaded successfully");
        }

        [HttpPost("update-trello")]
        public async Task<IActionResult> UpdateTrello()
        {
            await _tenderService.UpdateTrelloAsync();

            return Ok("Trello update process started");
        }

        [HttpGet]
        public async Task<IActionResult> GetTenders()
        {
            var tenders = await _tenderService.GetAllTendersAsync();
            return Ok(tenders);
        }
    }
}
