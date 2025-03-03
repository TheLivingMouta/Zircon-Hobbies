using Microsoft.EntityFrameworkCore;
using Zircon_Hobbies.Data;

namespace Zircon_Hobbies.Models
{
    public class GunplaSeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Zircon_HobbiesContext(
                serviceProvider.GetRequiredService<DbContextOptions<Zircon_HobbiesContext>>()))
            {

                if (context.Gunpla.Any())
                {
                    return;
                }

                context.Gunpla.AddRange(
                    new Gunpla
                    {
                        Name = "RX-78-2 Gundam Ver 2",
                        Type = "RG",
                        Scale = "1/144",
                        ProductionCompanyId = 1,
                        Price = 35.00,
                    },

                    new Gunpla
                    {

                        Name = "RX-124 Gundam TR-6 (Woundwort)",
                        Type = "HGUC",
                        Scale = "1/144",
                        ProductionCompanyId = 1,
                        Price = 34.00,

                    },

                    new Gunpla
                    {

                        Name = "ASW-G-08 Gundam Barbatos",
                        Type = "MG",
                        Scale = "1/100",
                        ProductionCompanyId = 1,
                        Price = 44.65,

                    }

                    new Gunpla
                    {

                        Name = "RX-124 Gundam TR-6 (Woundwort)",
                        Type = "HGUC",
                        Scale = "1/144",
                        ProductionCompanyId = 1,
                        Price = 34.00,

                    },

                    new Gunpla
                    {

                        Name = "Moderoid Villkiss",
                        Type = "N/A",
                        Scale = "N/A",
                        ProductionCompanyId = 2,
                        Price = 62.00,

                    },

                    new Gunpla
                    {

                        Name = "Moderoid AV-98 Ingram",
                        Type = "N/A",
                        Scale = "1/60",
                        ProductionCompanyId = 2,
                        Price = 38.00,

                    },

                    new Gunpla
                    {

                        Name = "Moderoid Linbarrel Overdirve",
                        Type = "N/A",
                        Scale = "N/A",
                        ProductionCompanyId = 2,
                        Price = 70.00,

                    }

                    );
                    
                context.SaveChanges();

            }
        } 

    }
}
