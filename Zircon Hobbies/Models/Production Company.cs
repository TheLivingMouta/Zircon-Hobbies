namespace Zircon_Hobbies.Models
{
    public class Production_Company
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Gunpla> Gunplas { get; set; } = new List<Gunpla>();

    }
}
