using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security;

namespace Zircon_Hobbies.Models
{
    public class GunplaViewModel
    {

        public List<Gunpla>? Gunplas { get; set; }
        public SelectList? Types { get; set; }
        public SelectList? Scale { get; set; }
        public string? GunplaType { get; set; }
        public string? GunplaScale { get; set; }
        public string? SearchString { get; set; }

    }
}
