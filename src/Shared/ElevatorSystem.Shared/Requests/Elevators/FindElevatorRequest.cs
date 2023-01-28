using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.RequestResponse.Requests;
using System;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Elevators
{
    public class FindElevatorRequest : RequestAbstract, IEntityAddRequest
    {
        public Guid BankId { get; set; }
        public Guid ElevatorId { get; set; }
    }
}