using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Zircon_Hobbies.Models
{
    public class Gunpla
    {

    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Scale { get; set; }
    public int ProductionCompanyId { get; set; }

    public Production_Company ProductionCompany { get; set; }
 
    public double Price { get; set; }


    }
}
