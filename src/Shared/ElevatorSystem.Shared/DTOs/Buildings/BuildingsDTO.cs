using System.Collections.Generic;

namespace IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings
{
    public class BuildingsDTO : IDataTransferObject
    {
        public List<BuildingDTO> Buildings { get; set; }
            = new List<BuildingDTO>();
    }
}