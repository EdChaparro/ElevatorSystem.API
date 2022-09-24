using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.WebAPI.Models
{
    public class BuildingsModel
    {
        public List<Building> Buildings { get; set; }
            = new List<Building>();

        public static BuildingsModel MapFrom(BuildingsDTO dto)
        {
            return new BuildingsModel
            {
                Buildings = dto.Buildings
                    .Select(Building.MapFrom)
                    .ToList()
            };
        }
    }
}