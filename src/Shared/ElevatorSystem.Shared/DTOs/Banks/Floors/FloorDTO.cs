using System.ComponentModel.DataAnnotations;

namespace IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks.Floors
{
    public class FloorDTO : IDataTransferObject
    {
        [Required]
        public int Number { get; set; }

        public string? Name { get; set; }
        public bool IsLocked { get; set; }


        public bool IsUpCallBackRequested { get; set; }
        public bool IsDownCallBackRequested { get; set; }
    }
}