using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponse.Responses;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class FindBuildingResponse : ResponseAbstract
    {
        public FindBuildingResponse(FindBuildingRequest request) : base(request)
        { }

        public BuildingDTO? Building { get; set; }
    }
}