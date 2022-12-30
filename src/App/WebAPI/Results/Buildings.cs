using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using System;
using System.Collections.Generic;

namespace IntrepidProducts.WebAPI.Results
{
    public class Buildings : List<Building>
    { }

    public class Building
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public static Building MapFrom(BuildingDTO dto)
        {
            return new Building
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}