using System;
using IntrepidProducts.RequestResponse.Requests;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Banks
{
    public class StartBankRequest : RequestAbstract
    {
        public Guid BankId { get; set; }
    }
}