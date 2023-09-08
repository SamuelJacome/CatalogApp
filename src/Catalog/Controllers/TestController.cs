using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}