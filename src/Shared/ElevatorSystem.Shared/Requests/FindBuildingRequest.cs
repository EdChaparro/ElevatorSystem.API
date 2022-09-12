using System;
using IntrepidProducts.RequestResponse.Requests;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests
{
    public class FindBuildingRequest : RequestAbstract, IEntityAddRequest
    {
        public Guid? BuildingId { get; set; }
    }
}