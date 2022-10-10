using IntrepidProducts.RequestResponse.Requests;
using System;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests
{
    public class DeleteBuildingRequest : RequestAbstract, IEntityAddRequest
    {
        public Guid? BuildingId { get; set; }
    }
}