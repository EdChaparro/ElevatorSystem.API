using System.ComponentModel.DataAnnotations;

namespace IntrepidProducts.WebAPI.Models
{
    public class BuildingName
    {
        [Required]
        [MinLength(1)]
        public string? Name { get; set; }
    }
}