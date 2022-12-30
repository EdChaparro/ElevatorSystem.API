using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.RequestResponse.Responses;
using System.Collections.Generic;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class FindBuildingResponse : ResponseAbstract
    {
        public FindBuildingResponse(FindBuildingRequest request) : base(request)
        { }

        public BuildingDTO? Building { get; set; }

        public List<BankDTO> Banks { get; set; } = new List<BankDTO>();
    }
}