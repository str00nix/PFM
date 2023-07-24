using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFM_API.Models;
using PFM_API.Services;

namespace PFM_API.Controllers
{
    [ApiController]
    [Route("v1/transactions")]
    public class TransactionController : Controller
    {

        ITransactionService _transactionService;
        private readonly ILogger<CategoryController> _logger;


        public TransactionController(ITransactionService transactionService, ILogger<CategoryController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }


        // v1/transactions/ 
        [HttpGet]
        public async Task<IActionResult> GetTransactionsAsync(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] SortOrder sortOrder = SortOrder.Asc,
            [FromQuery] string? sortBy = null)
        {
            var transactions = await _transactionService.GetTransactions(page, pageSize, sortOrder, sortBy);
            return Ok(transactions);
        }

        // v1/transactions/import
        [HttpPost("import")]
        public async Task<IActionResult> ImportTransactionsFromCSV([FromForm] IFormFile formFile)
        {
            Console.WriteLine("transaction controller import called");
            try
            {
                formFile = formFile ?? Request.Form.Files[0];
                var result = _transactionService.ImportTransactions(formFile);

                if (result == null)
                {
                    return StatusCode(500, "Error when importing transactions from inserted CSV file");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception thrown during transaction import from CSV");
                return StatusCode(500, "Error when importing transactions from inserted CSV file");
            }
        }
    }
}
