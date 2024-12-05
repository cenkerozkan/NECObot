#nullable disable
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Services;
using BLL.Models;
using BLL.Services.Bases;
using BLL.DAL;  

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
        
        public IActionResult AboutNeco()
        {
            return View();
        }
    }
}
