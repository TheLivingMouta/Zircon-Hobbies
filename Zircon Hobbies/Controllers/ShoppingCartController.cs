using Microsoft.AspNetCore.Mvc;
using Zircon_Hobbies.Data;
using Zircon_Hobbies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Zircon_Hobbies.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly Zircon_HobbiesContext _context;
        private readonly List<cartItem> _cartItems;

        public ShoppingCartController(Zircon_HobbiesContext context)
        {
            _context = context;
            _cartItems = new List<cartItem>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int id) 
        {
            
            var GunplaToAdd = _context.Gunpla.Find(id);

            var cartItems = HttpContext.Session.Get<List<cartItem>>("Cart") ?? new List<cartItem>();

            var existingCartItem = cartItems.FirstOrDefault(item => item.Gunpla.Id == id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                cartItems.Add(new cartItem
                {
                    Gunpla = GunplaToAdd,
                    Quantity = 1
                });
            }

            HttpContext.Session.Set("Cart", cartItems);

            TempData["CartMessage"] = $"{GunplaToAdd.Name} added to cart"; 

            return RedirectToAction("ViewCart");

        }

        public IActionResult ViewCart()
        {

            var cartItems = HttpContext.Session.Get<List<cartItem>>("Cart") ?? new List<cartItem>();

            foreach (var item in cartItems)
            {
                item.Gunpla = _context.Gunpla
                    .Include(g => g.ProductionCompany)
                    .FirstOrDefault(g => g.Id == item.Gunpla.Id);
            }

            var cartViewModel = new ShoppingCartView
            {
                cartItems = cartItems,
                TotalPrice = cartItems.Sum(item => item.Gunpla.Price * item.Quantity)
            };

            ViewBag.CartMessage = TempData["CartMessage"];

            return View(cartViewModel);
        }

        public IActionResult RemoveItem(int id)
        {
            var cartItems = HttpContext.Session.Get<List<cartItem>>("Cart") ?? new List<cartItem>();

            var itemToRemove = cartItems.FirstOrDefault(item => item.Gunpla.Id == id);

            TempData["CartMessage"] = $"{itemToRemove.Gunpla.Name} removed from cart";

            if (itemToRemove.Quantity > 1)
            {
                itemToRemove.Quantity--;
            }
            else
            {
                
                cartItems.Remove(itemToRemove);
            }

            HttpContext.Session.Set("Cart", cartItems);

            return RedirectToAction("ViewCart");
        }

        public IActionResult Checkout()
        {
            var cartItems = HttpContext.Session.Get<List<cartItem>>("Cart") ?? new List<cartItem>();

            if (cartItems.Count == 0)
            {
                TempData["CartMessage"] = $"Purchase Unsuccesful";
            } else
            {
                TempData["CartMessage"] = $"Purchase Succesful";
            }

            foreach (var item in cartItems)
            {
                _context.Purchases.Add(new Purchase
                {
                    GunplaId = item.Gunpla.Id,
                    Quantity = item.Quantity,
                    PurchaseDate = DateTime.Now,
                    Total = item.Gunpla.Price * item.Quantity
                });

              

            }

            _context.SaveChanges();

            ViewBag.CartMessage = TempData["CartMessage"];

            HttpContext.Session.Set("Cart", new List<cartItem>());

            return RedirectToAction("Index", "Home");

        }
    }
}
