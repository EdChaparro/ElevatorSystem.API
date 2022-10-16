using System;
using System.ComponentModel.DataAnnotations;

namespace IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings
{
    public class BuildingDTO
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        public string? Name { get; set; }
    }
}