using IntrepidProducts.RequestResponse.Requests;
using System;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Banks
{
    public class StartBankRequest : RequestAbstract
    {
        public Guid BuildingId { get; set; }
        public Guid BankId { get; set; }
    }
}