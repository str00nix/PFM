using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFM_API.Models;
using PFM_API.Services;

namespace PFM_API.Controllers
{
    [ApiController]
    [EnableCors("MyCORSPolicy")]
    [Route("v1/categories")]
    public class CategoryController : Controller
    {

        ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }


        // v1/categories
        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync(
            [FromQuery] int page = 1,
            [FromQuery(Name = "page-size")] int pageSize = 10,
            [FromQuery(Name = "sort-order")] SortOrder sortOrder = SortOrder.Asc,
            [FromQuery(Name = "sort-by")] string? sortBy = null,
            [FromQuery(Name = "parent-code")] string? parentCode = null)
        {
            var categories = await _categoryService.GetCategories(page, pageSize, sortOrder, sortBy, parentCode);
            return Ok(categories);
        }

        // v1/categories/import
        [HttpPost("import")]
        public async Task<IActionResult> ImportCategoriesFromCSV([FromForm] IFormFile formFile)
        {
            Console.WriteLine("category controller import called");
            try {
                formFile = formFile ?? Request.Form.Files[0];
                var result = _categoryService.ImportCategories(formFile);

                if (result == null)
                {
                    //return BadRequest("Error when importing categories from inserted CSV file");
                    return StatusCode(500, "Error when importing categories from inserted CSV file");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception thrown during category import from CSV");
                return StatusCode(500, "Error when importing categories from inserted CSV file");
            }
        }
    }
}
