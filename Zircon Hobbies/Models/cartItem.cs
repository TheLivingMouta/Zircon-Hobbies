using System.ComponentModel.DataAnnotations;

namespace Zircon_Hobbies.Models
{
    public class cartItem
    {

        public int Id { get; set; }

        public Gunpla Gunpla { get; set; }

        public int Quantity { get; set; }

    }
}
