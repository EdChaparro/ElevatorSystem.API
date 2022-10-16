using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.RequestResponse.Requests;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings
{
    public class AddBuildingRequest : RequestAbstract, IEntityAddRequest
    {
        public BuildingDTO? Building { get; set; }
    }
}