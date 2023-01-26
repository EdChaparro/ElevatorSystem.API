using System;
using System.Collections.Generic;

namespace IntrepidProducts.ElevatorSystem.Shared.DTOs.Elevators
{
    public class ElevatorDTO : IDataTransferObject
    {
        public ElevatorDTO()
        {
            FloorNbrs = new List<int>();
        }

        public Guid Id { get; set; }

        public Guid BankId { get; set; }

        public string? Name { get; set; }

        public int CurrentFloorNumber { get; set; }
        public List<int> FloorNbrs { get; set; }

        public bool IsEnabled { get; set; }
        public bool IsIdle { get; set; }
    }
}
