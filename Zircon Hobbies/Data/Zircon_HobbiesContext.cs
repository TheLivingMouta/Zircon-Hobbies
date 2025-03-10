using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zircon_Hobbies.Models;

namespace Zircon_Hobbies.Data
{
    public class Zircon_HobbiesContext : DbContext
    {
        public Zircon_HobbiesContext (DbContextOptions<Zircon_HobbiesContext> options)
            : base(options)
        {
        }

        public DbSet<Zircon_Hobbies.Models.Gunpla> Gunpla { get; set; } = default!;
        public DbSet<Zircon_Hobbies.Models.Production_Company> Production_Company { get; set; } = default!;
        public DbSet<cartItem> ShoppingCartItems { get; set; } = default!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = default!;
    }
}
