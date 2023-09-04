using Catalog.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var apiConfig = new ApiConfiguration();
            _configuration.GetSection(ApiConfiguration.ConfigName).Bind(apiConfig);

            var secret = apiConfig.UserSecret;

            return View();
        }
    }
}