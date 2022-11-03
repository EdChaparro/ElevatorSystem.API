using IntrepidProducts.RequestResponse.Requests;
using System;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings
{
    public class DeleteBuildingRequest : RequestAbstract, IEntityAddRequest
    {
        public Guid BuildingId { get; set; }
    }
}