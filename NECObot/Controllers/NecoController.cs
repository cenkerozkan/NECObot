using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace NECObot.Controllers
{
    public class NecoController : Controller
    {
        private readonly ILogger<NecoController> _logger;
        public NecoController(ILogger<NecoController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TryNeco()
        {
            return View();
        }

    }
}
