using System;
using System.Collections.Generic;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Elevators;

namespace IntrepidProducts.WebAPI.Results
{
    public class Elevators : List<Elevator>
    { }

    public class Elevator
    {
        public Guid Id { get; set; }

        public Guid BankId { get; set; }

        public string? Name { get; set; }

        public int CurrentFloorNumber { get; set; }
        public List<int> FloorNbrs { get; set; } = new List<int>();

        public bool IsEnabled { get; set; }
        public bool IsIdle { get; set; }


        public static Elevator MapFrom(ElevatorDTO dto)
        {
            return new Elevator
            {
                Id = dto.Id,
                BankId = dto.BankId,
                Name = dto.Name,
                CurrentFloorNumber = dto.CurrentFloorNumber,
                FloorNbrs = dto.FloorNbrs,
                IsEnabled = dto.IsEnabled,
                IsIdle = dto.IsIdle
            };
        }
    }
}