using System.Collections.Generic;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponse.Responses;

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