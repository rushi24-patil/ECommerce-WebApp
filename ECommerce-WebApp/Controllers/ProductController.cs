using ECommerce_WebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_WebApp.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
