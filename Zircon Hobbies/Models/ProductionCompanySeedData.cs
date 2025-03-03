using Microsoft.EntityFrameworkCore;
using Zircon_Hobbies.Data;

namespace Zircon_Hobbies.Models
{
    public class ProductionCompanySeedData
    {

        public static void Initializer(IServiceProvider serviceProvider)
        {

            using (var context = new Zircon_HobbiesContext(
                serviceProvider.GetRequiredService<DbContextOptions<Zircon_HobbiesContext>>()))
            {

                if (context.Production_Company.Any())
                {
                    return;
                }

                context.Production_Company.AddRange(
                    new Production_Company
                    {
                        Name = "Bandai Spirits",
                        Location = "Japan"
                    },

                    new Production_Company
                    {
                        Name = "Good Smile Company",
                        Location = "Japan"
                    }
                    );

                context.SaveChanges();
            
            }

        }

    }
}
