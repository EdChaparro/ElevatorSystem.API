using System;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;

namespace IntrepidProducts.WebAPI.Models
{
    public class Building
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public Link? Link { get; set; }

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