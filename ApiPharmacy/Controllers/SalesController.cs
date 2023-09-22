
using Microsoft.AspNetCore.Mvc;


namespace ApiPharmacy.Controllers
{
    [Route("[controller]")]
    public class SalesController : Controller
    {
        private readonly ILogger<SalesController> _logger;

        public SalesController(ILogger<SalesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}