using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.RequestResponse.Requests;

namespace IntrepidProducts.ElevatorSystem.Shared.Requests.Banks
{
    public class AddBankRequest : RequestAbstract, IEntityAddRequest
    {
        public BankDTO? Bank { get; set; }
    }
}