using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.WebAPI.Results;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.WebAPI.Models
{
    public class BuildingCollection
    {
        public List<Building> Buildings { get; set; }
            = new List<Building>();

        public static BuildingCollection MapFrom(BuildingsDTO dto)
        {
            return new BuildingCollection
            {
                Buildings = dto.Buildings
                    .Select(Building.MapFrom)
                    .ToList()
            };
        }
    }
}