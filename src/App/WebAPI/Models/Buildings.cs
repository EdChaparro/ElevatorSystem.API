using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.WebAPI.Models
{
    public class Buildings
    {
        public List<Building> BuildingsInfo { get; set; }
            = new List<Building>();

        public static Buildings MapFrom(BuildingsDTO dto)
        {
            return new Buildings
            {
                BuildingsInfo = dto.Buildings
                    .Select(Building.MapFrom)
                    .ToList()
            };
        }
    }
}