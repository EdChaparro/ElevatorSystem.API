using IntrepidProducts.Common;
using IntrepidProducts.ElevatorSystem.Banks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IntrepidProducts.Shared.ElevatorSystem.Entities
{
    public class BuildingElevatorBank : IHasId
    {
        public BuildingElevatorBank(Guid buildingId, Bank bank)
        {
            Id = bank.Id;
            BuildingId = buildingId;

            Name = bank.Name;
            NumberOfElevators = bank.NumberOfElevators;
            LowestFloorNbr = bank.LowestFloorNbr;
            HighestFloorNbr = bank.HighestFloorNbr;
            FloorNbrs = bank.OrderedFloorNumbers.ToList();

            SetupElevators(bank);
        }

        public BuildingElevatorBank()
        {
            FloorNbrs = new List<int>();
        }
        public Guid Id { get; set; }

        public Guid BuildingId { get; set; }

        public string? Name { get; set; }

        #region Elevators

        private void SetupElevators(Bank bank)
        {
            Elevators = new List<Elevator>();

            foreach (var bankElevator in bank.Elevators)
            {
                Elevators.Add(new Elevator(bank.Id, bankElevator));
            }
        }

        [Required]
        [Range(2, 999)]
        public int NumberOfElevators { get; set; }

        public List<Elevator> Elevators { get; set; }
        #endregion

        #region Floors
        public int LowestFloorNbr { get; set; }
        public int HighestFloorNbr { get; set; }

        //Mutually exclusive with Lowest & Highest Floor Number properties
        public List<int> FloorNbrs { get; set; }
        #endregion

        public Bank ToBusinessObject()
        {
            var bank = new Bank(NumberOfElevators, FloorNbrs.ToArray())
            {
                Name = Name,
            };

            return bank;
        }
    }
}