using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.RequestResponse.Requests;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests
{
    public class UpdateBuildingRequest : RequestAbstract, IEntityAddRequest
    {
        public BuildingDTO? Building { get; set; }
    }
}