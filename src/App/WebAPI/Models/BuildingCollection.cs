using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.WebAPI.Models
{
    public class BuildingCollection
    {
        public List<Results.Building> Buildings { get; set; }
            = new List<Results.Building>();

        public static BuildingCollection MapFrom(BuildingsDTO dto)
        {
            return new BuildingCollection
            {
                Buildings = dto.Buildings
                    .Select(Results.Building.MapFrom)
                    .ToList()
            };
        }
    }
}