using Microsoft.AspNetCore.Mvc;

namespace ECommerce_WebApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
