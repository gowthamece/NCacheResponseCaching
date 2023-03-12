using Microsoft.AspNetCore.Mvc;
using NCacheResponseCaching.Models;
using System.Diagnostics;

namespace NCacheResponseCaching.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHotelService _hotelService;
        public HomeController(IHotelService hotelservice) 
        {
            _hotelService = hotelservice;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetHoteList()
        {
            return View(await _hotelService.GetHotels()); 
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}