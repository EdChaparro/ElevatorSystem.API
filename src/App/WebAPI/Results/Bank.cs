using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using System;
using System.Collections.Generic;

namespace IntrepidProducts.WebAPI.Results
{
    public class Bank
    {
        public Guid Id { get; set; }

        public Guid BuildingId { get; set; }

        public string? Name { get; set; }

        public int NumberOfElevators { get; set; }

        public int LowestFloorNbr { get; set; }
        public int HighestFloorNbr { get; set; }
        public List<int> FloorNbrs { get; set; } = new List<int>();

        public Link? Link { get; set; }

        public static Bank MapFrom(BankDTO dto)
        {
            return new Bank
            {
                Id = dto.Id,
                BuildingId = dto.BuildingId,
                Name = dto.Name,
                NumberOfElevators = dto.NumberOfElevators,
                LowestFloorNbr = dto.LowestFloorNbr,
                HighestFloorNbr = dto.HighestFloorNbr,
                FloorNbrs = dto.FloorNbrs
            };
        }
    }
}