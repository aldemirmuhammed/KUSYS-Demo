using Microsoft.AspNetCore.Mvc; 

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        #region Members
        private readonly ILogger<HomeController> _logger;
        #endregion

        #region Constructor
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        #endregion


        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        #endregion

    }
}