using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.ElevatorSystemBiz.Mappers;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks
{
    public class StartBankRequestHandler :
        AbstractRequestHandler<StartBankRequest, BankOperationsResponse>
    {
        public StartBankRequestHandler(IRepository<BuildingElevatorBank> bankRepo)
        {
            _bankRepo = bankRepo;
        }

        private readonly IRepository<BuildingElevatorBank> _bankRepo;

        protected override BankOperationsResponse DoHandle(StartBankRequest request)
        {
            var bankEntity = _bankRepo.FindById(request.BankId);

            if (bankEntity == null)
            {
                return new BankOperationsResponse(request);
            }

            var elevatorDTOs = ElevatorMapper.Map(bankEntity.Elevators);

            return new BankOperationsResponse(request) { ElevatorDTOs = elevatorDTOs };
        }

        //TODO: Finish Me
        private bool StartBank(BuildingElevatorBank bank)
        {
            return false;
        }
    }
}