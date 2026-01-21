using Microsoft.AspNetCore.Mvc;

namespace ECommerce_WebApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task< IActionResult> AddToCart(int ProdutId)
        {
            return null;
        }
    }
}
