namespace Zircon_Hobbies.Models
{
    public class ShoppingCartView
    {

        public List<cartItem> cartItems {  get; set; }

        public double? TotalPrice { get; set; }
        public int? TotalQuantity { get; set; }

    }
}
