using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.RequestResponse.Requests;
using System;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Banks
{
    public class FindAllBanksRequest : RequestAbstract, IEntityAddRequest
    {
        public Guid BuildingId { get; set; }
    }
}