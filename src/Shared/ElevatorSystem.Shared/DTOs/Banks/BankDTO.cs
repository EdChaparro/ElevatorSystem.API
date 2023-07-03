using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks
{
    public class BankDTO : IDataTransferObject
    {
        public BankDTO()
        {
            FloorNbrs = new List<int>();
        }
        public Guid Id { get; set; }

        public Guid BuildingId { get; set; }

        public string? Name { get; set; }

        [Required]
        [Range(2, 999)]
        public int NumberOfElevators { get; set; }

        #region Floors
        public int LowestFloorNbr { get; set; }
        public int HighestFloorNbr { get; set; }

        //Mutually exclusive with Lowest & Highest Floor Number properties
        public List<int> FloorNbrs { get; set; }
        #endregion

        public bool IsRunning { get; set; }
    }
}