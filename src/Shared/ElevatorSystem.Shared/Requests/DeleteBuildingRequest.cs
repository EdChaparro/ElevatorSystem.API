using System;
using IntrepidProducts.RequestResponse.Requests;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests
{
    public class DeleteBuildingRequest : RequestAbstract, IEntityAddRequest
    {
        public Guid? BuildingId { get; set; }
    }
}