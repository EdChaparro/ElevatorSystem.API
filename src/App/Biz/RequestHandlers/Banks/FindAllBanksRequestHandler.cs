using IntrepidProducts.ElevatorService.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.ElevatorSystemBiz.Mappers;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;

namespace IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks
{
    public class FindAllBanksRequestHandler :
        AbstractRequestHandler<FindAllBanksRequest, FindEntitiesResponse<BankDTO>>
    {
        public FindAllBanksRequestHandler
            (IBuildingElevatorBankRepository bankRepo,
                IBankServiceRegistry bankServiceRegistry)

        {
            _bankRepo = bankRepo;
            _bankServiceRegistry = bankServiceRegistry;
        }

        private readonly IBuildingElevatorBankRepository _bankRepo;
        private readonly IBankServiceRegistry _bankServiceRegistry;

        protected override FindEntitiesResponse<BankDTO> DoHandle(FindAllBanksRequest request)
        {
            var banks = _bankRepo.FindByBusinessId(request.BuildingId);

            var bankDTOs = BankMapper.Map(banks);

            foreach (var bankDtO in bankDTOs)
            {
                bankDtO.IsRunning = _bankServiceRegistry.IsRegistered(bankDtO.Id);
            }

            return new FindEntitiesResponse<BankDTO>(request) { Entities = bankDTOs };
        }
    }
}