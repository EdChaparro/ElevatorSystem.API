using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.RequestResponse.Requests;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests
{
    public class AddBuildingRequest : RequestAbstract, IEntityAddRequest
    {
        public BuildingDTO? Building { get; set; }
    }
}