namespace Zircon_Hobbies.Models
{
    public class Purchase
    {

        public int Id { get; set; }
        public int GunplaId { get; set; }
        public Gunpla Gunpla { get; set;}
        public  int? Quantity { get; set; }
        public DateTime? PurchaseDate { get; set; }

        public double? Total { get; set; }

    }
}
