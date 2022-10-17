using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.RequestResponse.Requests;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings
{
    public class UpdateBuildingRequest : RequestAbstract, IEntityAddRequest
    {
        public BuildingDTO? Building { get; set; }
    }
}