using System;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.RequestResponse.Requests;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Banks
{
    public class FindBankRequest : RequestAbstract, IEntityAddRequest
    {
        public Guid BankId { get; set; }
    }
}