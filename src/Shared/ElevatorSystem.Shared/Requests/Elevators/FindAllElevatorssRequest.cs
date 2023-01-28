using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.RequestResponse.Requests;
using System;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Elevators
{
    public class FindAllElevatorsRequest : RequestAbstract, IEntityAddRequest
    {
        public Guid BankId { get; set; }
    }
}