﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFM_API.Commands;
using PFM_API.Models;
using PFM_API.Services;

namespace PFM_API.Controllers
{
    [ApiController]
    [EnableCors("MyCORSPolicy")]
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
            [FromQuery(Name = "transaction-kind")] string? transactionKindQuery = null,
            [FromQuery(Name = "start-date")] DateTime? startDate = null,
            [FromQuery(Name = "end-date")] DateTime? endDate = null,
            [FromQuery] int page = 1,
            [FromQuery(Name = "page-size")] int pageSize = 10,
            [FromQuery(Name = "sort-order")] SortOrder sortOrder = SortOrder.Asc,
            [FromQuery(Name = "sort-by")] string? sortBy = null)
        {
            List<TransactionKindEnum> listOfKinds = new List<TransactionKindEnum>();
            if (transactionKindQuery != null)
            {
                string[] transactionKinds = transactionKindQuery.Split(',');
                foreach (string s in transactionKinds)
                {
                    TransactionKindEnum transactionKindEnum = new TransactionKindEnum();
                    Enum.TryParse<TransactionKindEnum>(s, out transactionKindEnum);
                    listOfKinds.Add(transactionKindEnum);
                }
            }

            if (pageSize > 25) {
                pageSize = 25;
            }

            var transactions = await _transactionService.GetTransactions(listOfKinds, startDate, endDate, page, pageSize, sortOrder, sortBy);
            return Ok(transactions);
        }

        static bool RowHasData(List<string> cells)
        {
            return cells.Any(x => x.Length > 0);
        }

        static double parseStringToDouble(string str)
        {
            return double.Parse(str.Trim('"').Replace(",", ""));
        }

        // v1/transactions/import
        [HttpPost("import")]
        public async Task<IActionResult> ImportTransactionsFromCSV([FromForm] IFormFile formFile)
        {
            Console.WriteLine("transaction controller import called");
            try
            {
                formFile = formFile ?? Request.Form.Files[0];
                //var result = _transactionService.ImportTransactions(formFile);

                //if (result == null)
                //{
                //    return StatusCode(500, "Error when importing transactions from inserted CSV file");
                //}

                using (var reader = new StreamReader(formFile.OpenReadStream()))
                {

                    bool firstLine = true;
                    while (reader.EndOfStream == false)
                    {
                        var content = reader.ReadLine();
                        try {

                        var parts = content.Split(',').ToList();

                            //Console.WriteLine(cells);
                            //break;

                            if (RowHasData(parts))
                        {

                            if (!firstLine)
                            {

                                string Id = parts[0];

                                string beneficiaryName = parts[1];

                                string[] dateParts = parts[2].Split('/');
                                DateTime date = new DateTime(int.Parse(dateParts[2]), int.Parse(dateParts[0]), int.Parse(dateParts[1]));

                                DirectionsEnum directions;
                                Enum.TryParse<DirectionsEnum>(parts[3], out directions);

                                double amount = parts.Capacity == 9 ? double.Parse(parts[4]) : parseStringToDouble(parts[4] + parts[5]);

                                string description = parts.Capacity == 9 ? parts[5] : parts[6];

                                string currencyCode = parts.Capacity == 9 ? parts[6] : parts[7];

                                //MCCEnum mccEnum;
                                //Enum.TryParse<MCCEnum>(parts.Capacity == 9 ? parts[7] : parts[8], out mccEnum);

                                TransactionKindEnum kind;
                                Enum.TryParse<TransactionKindEnum>(parts.Capacity == 9 ? parts[8] : parts[9], out kind);

                                var inserted = await _transactionService.InsertTransaction(new Transaction()
                                {
                                    Id = Id,
                                    BeneficiaryName = beneficiaryName,
                                    Date = date,
                                    Amount = amount,
                                    Direction = directions,
                                    Description = description,
                                    CurrencyCode = currencyCode,
                                    //Mcc = mccEnum,
                                    Mcc = parts.Capacity == 9 ? parts[7] : parts[8],
                                    Kind = kind
                                });
                                    if (inserted == false) {
                                        continue;
                                    };
                            }
                            firstLine = false;
                        }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(content);
                            Console.WriteLine(ex.Message);
                            //content
                            break;
                        }
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception thrown during transaction import from CSV");
                return StatusCode(500, "Error when importing transactions from inserted CSV file");
            }
        }

        [HttpPost("{id}/categorize")]
        public async Task<IActionResult> CategorizeTransaction([FromRoute] string id, [FromBody] CategorizeTransactionCommand command)
        {
            var categorised = await _transactionService.CategorizeTransaction(id, command.catcode);
            if (categorised){
                return Ok("Categorisation completed");
            }
            else{
                return StatusCode(500, "Failed to categorize transaction");
            }
        }

        //v1/transactions/auto-categorize
        [HttpPost("auto-categorize")]
        //[NonAction]
        public async Task<IActionResult> AutoCategorizeTransactions()
        {
            bool success = await _transactionService.AutoCategorizeTransactions();
            if (success)
            {
                return Ok("Auto-categorization completed");
            }
            else
            {
                return StatusCode(500, "Failed to auto-categorize all transactions");
            }
        }

        [HttpGet("spending-analytics")]
        public async Task<IActionResult> GetSpendingAnalytics([FromQuery] string? catcode = null, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null, [FromQuery] DirectionsEnum? directionKind = null)
        {
            List<SpendingByCategory> list = await _transactionService.GetAnaliytics(catcode, startDate, endDate, directionKind);
            return Ok(list);
        }

        [HttpPost("{id}/split")]
        public async Task<IActionResult> SplitTransaction([FromRoute] string id, [FromBody] SplitTransactionCommand splits)
        {
            var splitResult = await _transactionService.SplitTransaction(id, splits.Splits.ToList());
            if (splitResult == false) return StatusCode(500, "Could not be split");
            return Ok("Split completed succesfully");
        }
    }
}
