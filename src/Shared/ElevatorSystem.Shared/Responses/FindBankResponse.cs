using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.RequestResponse.Responses;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class FindBankResponse : ResponseAbstract
    {
        public FindBankResponse(FindBankRequest request) : base(request)
        { }

        public BankDTO? Bank { get; set; }
    }
}