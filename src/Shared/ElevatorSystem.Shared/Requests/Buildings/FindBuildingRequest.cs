using IntrepidProducts.RequestResponse.Requests;
using System;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings
{
    public class FindBuildingRequest : RequestAbstract, IEntityAddRequest
    {
        public Guid? BuildingId { get; set; }
    }
}