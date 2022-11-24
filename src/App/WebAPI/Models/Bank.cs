using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using System.Collections.Generic;
using System;

namespace IntrepidProducts.WebAPI.Models
{
    public class Bank
    {
        public Guid BuildingId { get; set; }

        public string? Name { get; set; }

        public int NumberOfElevators { get; set; }

        public int LowestFloorNbr { get; set; }
        public int HighestFloorNbr { get; set; }
        public List<int> FloorNbrs { get; set; } = new List<int>();

        public static Bank MapFrom(BankDTO dto)
        {
            return new Bank
            {
                BuildingId = dto.BuildingId,
                Name = dto.Name,
                NumberOfElevators = dto.NumberOfElevators,
                LowestFloorNbr = dto.LowestFloorNbr,
                HighestFloorNbr = dto.HighestFloorNbr,
                FloorNbrs = dto.FloorNbrs
            };
        }

        public BankDTO MapTo()
        {
            return new BankDTO
            {
                BuildingId = BuildingId,
                Name = Name,
                NumberOfElevators = NumberOfElevators,
                LowestFloorNbr = LowestFloorNbr,
                HighestFloorNbr = HighestFloorNbr,
                FloorNbrs = FloorNbrs
            };
        }
    }
}