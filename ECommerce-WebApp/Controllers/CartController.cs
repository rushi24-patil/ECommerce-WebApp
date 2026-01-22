using ECommerce_WebApp.Data;
using ECommerce_WebApp.Helpers;
using ECommerce_WebApp.Models;
using Microsoft.AspNetCore.Mvc;

public class CartController : Controller
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObject<Cart>("Cart") ?? new Cart();

        var productIds = cart.CartItems.Select(i => i.ProductId).ToList();

        var products = _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionary(p => p.Id);

        ViewBag.Products = products;

        return View(cart);
    }


    public IActionResult AddToCart(int productId)
    {
        var cart = HttpContext.Session.GetObject<Cart>("Cart") ?? new Cart();

        var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
            item.Quantity++;
        else
            cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = 1 });

        HttpContext.Session.SetObject("Cart", cart);

        TempData["Message"] = "Product added to cart!";
        return RedirectToAction("Index", "Product");
    }
}
