using ECommerce_WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToList();

            ViewData["Categories"] = _context.Categories.ToList();
            ViewData["Brands"] = _context.Brands.ToList();

            return View(products);
        }
        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


    }
}
