using Microsoft.AspNetCore.Mvc;
using Zircon_Hobbies.Data;
using Zircon_Hobbies.Models;

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

            var existingCartItem = _cartItems.FirstOrDefault(item => item.Gunpla.Id == id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                _cartItems.Add(new cartItem
                {
                    Gunpla = GunplaToAdd,
                    Quantity = 1
                });
            }

            return RedirectToAction("ViewCart");

        }

        public IActionResult ViewCart()
        {

            var cartViewModel = new ShoppingCartView
            {
                cartItems = _cartItems,
                TotalPrice = _cartItems.Sum(item => item.Gunpla.Price)
            };

            return View(cartViewModel);
        }

    }
}
