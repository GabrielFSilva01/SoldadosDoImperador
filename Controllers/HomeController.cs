using Microsoft.AspNetCore.Mvc;
using SoldadosDoImperador.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace SoldadosDoImperador.Controllers
{
    [Authorize]
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        
        public IActionResult Index()
        {
            return View();
        }

  
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

     
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}