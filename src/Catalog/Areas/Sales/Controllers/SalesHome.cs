using Microsoft.AspNetCore.Mvc;

namespace Catalog.Areas.Controllers.Sales
{
    [Area("Sales")]
    public class SalesHome : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}