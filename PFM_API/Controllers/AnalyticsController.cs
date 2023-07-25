using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PFM_API.Controllers
{
    [ApiController]
    [EnableCors("MyCORSPolicy")]
    [Route("v1/spending-analytics")]
    public class AnalyticsController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
