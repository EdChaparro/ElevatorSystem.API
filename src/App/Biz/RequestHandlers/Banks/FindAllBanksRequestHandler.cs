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
        public FindAllBanksRequestHandler(IBuildingElevatorBankRepository bankRepo)
        {
            _bankRepo = bankRepo;
        }

        private readonly IBuildingElevatorBankRepository _bankRepo;

        protected override FindEntitiesResponse<BankDTO> DoHandle(FindAllBanksRequest request)
        {
            var banks = _bankRepo.FindByBusinessId(request.BuildingId);

            var bankDTOs = BankMapper.Map(banks);

            return new FindEntitiesResponse<BankDTO>(request) { Entities = bankDTOs };
        }

    }
}