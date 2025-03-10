using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zircon_Hobbies.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<cartItem> cartItems { get; set; }

    }
}
