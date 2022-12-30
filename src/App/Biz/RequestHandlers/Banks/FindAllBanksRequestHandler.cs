using IntrepidProducts.Biz.Mappers;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;

namespace IntrepidProducts.Biz.RequestHandlers.Banks
{
    public class FindAllBanksRequestHandler :
        AbstractRequestHandler<FindAllBanksRequest, FindEntityResponse<BankDTO>>
    {
        public FindAllBanksRequestHandler(IBuildingElevatorBankRepository bankRepo)
        {
            _bankRepo = bankRepo;
        }

        private readonly IBuildingElevatorBankRepository _bankRepo;

        protected override FindEntityResponse<BankDTO> DoHandle(FindAllBanksRequest request)
        {
            var banks = _bankRepo.FindByBusinessId(request.BuildingId);

            var bankDTOs = BankMapper.Map(banks);

            return new FindEntityResponse<BankDTO>(request) { Entities = bankDTOs };
        }

    }
}