using IntrepidProducts.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.Shared.ElevatorSystem.Entities
{
    public class Elevator : IHasId
    {
        public Elevator(Guid bankId, IntrepidProducts.ElevatorSystem.Elevators.Elevator elevator)
        {
            Id = elevator.Id;
            BankId = bankId;

            Name = elevator.Name;

            FloorNbrs = elevator.OrderedFloorNumbers.ToList();
            CurrentFloorNumber = elevator.CurrentFloorNumber;

            IsEnabled = elevator.IsEnabled;
            IsIdle = elevator.IsIdle;
        }
        public Elevator()
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