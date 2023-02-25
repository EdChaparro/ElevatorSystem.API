using IntrepidProducts.Biz.Mappers;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.Biz.RequestHandlers.Banks
{
    public class StartBankRequestHandler :
        AbstractRequestHandler<StartBankRequest, BankControlPanelResponse>
    {
        public StartBankRequestHandler(IRepository<BuildingElevatorBank> bankRepo)
        {
            _bankRepo = bankRepo;
        }

        private readonly IRepository<BuildingElevatorBank> _bankRepo;

        protected override BankControlPanelResponse DoHandle(StartBankRequest request)
        {
            var bankEntity = _bankRepo.FindById(request.BankId);

            if (bankEntity == null)
            {
                return new BankControlPanelResponse(request);
            }

            var elevatorDTOs = ElevatorMapper.Map(bankEntity.Elevators);

            return new BankControlPanelResponse(request) { ElevatorDTOs = elevatorDTOs };
        }

        //TODO: Finish Me
        private bool StartBank(BuildingElevatorBank bank)
        {
            return false;
        }
    }
}