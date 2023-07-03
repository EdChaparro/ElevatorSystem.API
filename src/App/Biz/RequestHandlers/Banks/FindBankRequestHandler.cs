using IntrepidProducts.ElevatorService.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks
{
    public class FindBankRequestHandler :
        AbstractRequestHandler<FindBankRequest, FindEntityResponse<BankDTO>>
    {
        public FindBankRequestHandler
            (IRepository<BuildingElevatorBank> bankRepo,
                IBankServiceRegistry bankServiceRegistry)
        {
            _bankRepo = bankRepo;
            _bankServiceRegistry = bankServiceRegistry;
        }

        private readonly IRepository<BuildingElevatorBank> _bankRepo;
        private readonly IBankServiceRegistry _bankServiceRegistry;

        protected override FindEntityResponse<BankDTO> DoHandle(FindBankRequest request)
        {
            var elevatorBank = _bankRepo.FindById(request.BankId);

            var dto = elevatorBank == null
                ? null
                : ToDTO(elevatorBank);

            return new FindEntityResponse<BankDTO>(request)
            {
                Entity = dto
            };
        }

        private BankDTO ToDTO(BuildingElevatorBank bank)
        {
            return new BankDTO
            {
                Id = bank.Id,
                BuildingId = bank.BuildingId,
                Name = bank.Name,
                NumberOfElevators = bank.NumberOfElevators,
                FloorNbrs = bank.FloorNbrs,
                LowestFloorNbr = bank.LowestFloorNbr,
                HighestFloorNbr = bank.HighestFloorNbr,
                IsRunning = _bankServiceRegistry.IsRegistered(bank.Id)
            };
        }
    }
}