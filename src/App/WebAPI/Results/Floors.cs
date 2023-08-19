using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks.Floors;
using System.Collections.Generic;

namespace IntrepidProducts.WebAPI.Results
{
    public class Floors : List<Floor>
    { }

    public class Floor
    {
        public int Number { get; set; }

        public string? Name { get; set; }
        public bool IsLocked { get; set; }

        public bool IsUpCallBackRequested { get; set; }
        public bool IsDownCallBackRequested { get; set; }


        public static Floor MapFrom(FloorDTO dto)
        {
            return new Floor
            {
                Number = dto.Number,
                Name = dto.Name ?? dto.Number.ToString(),
                IsDownCallBackRequested = dto.IsDownCallBackRequested,
                IsUpCallBackRequested = dto.IsUpCallBackRequested,
                IsLocked = dto.IsLocked
            };
        }
    }
}