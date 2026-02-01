using ECommerce_WebApp.Data;
using ECommerce_WebApp.Helpers;
using ECommerce_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObject<Cart>("Cart");

            if (cart == null || !cart.CartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var productIds = cart.CartItems.Select(c => c.ProductId).ToList();
            var products = _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionary(p => p.Id); 

            foreach (var item in cart.CartItems)
            {
                if (products.ContainsKey(item.ProductId))
                    item.Product = products[item.ProductId];
            }

            return View(cart);
        }

        [HttpPost]
        public IActionResult PlaceOrder(string shippingAddress, string city, string postalCode, string country)
        {
            var cart = HttpContext.Session.GetObject<Cart>("Cart");

            if (cart == null || !cart.CartItems.Any())
                return RedirectToAction("Index", "Cart");

            var productIds = cart.CartItems.Select(ci => ci.ProductId).ToList();
            var products = _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionary(p => p.Id);

            var order = new Order
            {
                UserId = 1,
                ShippingAddress = shippingAddress,
                City = city,
                PostalCode = postalCode,
                Country = country,
                TotalAmount = cart.CartItems.Sum(i => products[i.ProductId].Price * i.Quantity),
                OrderItems = cart.CartItems.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = products[i.ProductId].Price
                }).ToList(),
                Status = "Pending",
                OrderDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            HttpContext.Session.Remove("Cart");

            return RedirectToAction("OrderSuccess", new { id = order.Id });
        }

        public IActionResult OrderSuccess(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return RedirectToAction("Index", "Product");

            return View(order);
        }
    }
}
