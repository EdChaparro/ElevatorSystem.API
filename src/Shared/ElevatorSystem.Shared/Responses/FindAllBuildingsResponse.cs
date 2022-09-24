using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponse.Responses;
using System.Collections.Generic;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class FindAllBuildingsResponse : ResponseAbstract
    {
        public FindAllBuildingsResponse(FindAllBuildingsRequest request) : base(request)
        {
            Buildings = new List<BuildingDTO>();
        }

        public List<BuildingDTO> Buildings { get; set; }
    }
}