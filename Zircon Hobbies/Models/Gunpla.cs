using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Zircon_Hobbies.Models
{
    public class Gunpla
    {

    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Type is required")]
    public string Type { get; set; }

    [Required(ErrorMessage = "Scale is required")]
    public string Scale { get; set; }

    [Required(ErrorMessage = "Production Company is required")]
    public int ProductionCompanyId { get; set; }

    public Production_Company ProductionCompany { get; set; }
 
    [Required(ErrorMessage = "Price is required")]
    [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10,000.")]
    public double Price { get; set; }


    }
}
