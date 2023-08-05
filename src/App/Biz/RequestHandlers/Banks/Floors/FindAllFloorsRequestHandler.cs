using System.Linq;
using IntrepidProducts.ElevatorService.Banks;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks.Floors;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks.Floors;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.ElevatorSystemBiz.Mappers;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;

namespace IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks.Floors
{
    public class FindAllFloorsRequestHandler :
        AbstractRequestHandler<FindAllFloorsRequest, FindEntitiesResponse<FloorDTO>>
    {
        public FindAllFloorsRequestHandler
            (IBuildingElevatorBankRepository bankRepo,
                IBankServiceRegistry bankServiceRegistry)

        {
            _bankRepo = bankRepo;
            _bankServiceRegistry = bankServiceRegistry;
        }

        private readonly IBuildingElevatorBankRepository _bankRepo;
        private readonly IBankServiceRegistry _bankServiceRegistry;

        protected override FindEntitiesResponse<FloorDTO> DoHandle(FindAllFloorsRequest request)
        {
            var banks =
                _bankRepo.FindByBuildingId(request.BuildingId).ToList();

            var bank = banks.FirstOrDefault(x => x.Id == request.BankId);

            var response = new FindEntitiesResponse<FloorDTO>(request);

            if (bank == null)
            {
                return response;
            }

            var bankService = _bankServiceRegistry.Get(bank.Id);

            if (bankService == null)
            {
                var defaultDTO = FloorMapper
                    .Map(new Bank(bank.NumberOfElevators, bank.FloorNbrs.ToArray()));
                return new FindEntitiesResponse<FloorDTO>(request) { Entities = defaultDTO };
            }

            var bankEntity = bankService.Entity;

            var dtos = FloorMapper
                .Map(new Bank(bank.NumberOfElevators, bank.FloorNbrs.ToArray()));


            foreach (var dto in dtos)
            {
                var floorInfo = bankEntity.GetFloorFor(dto.Number);
                var panel = floorInfo?.Panel;
                if (panel == null)
                {
                    continue;
                }

                var downButton = panel.DownButton;
                var upButton = panel.UpButton;

                dto.IsDownCallBackRequested = downButton?.IsPressed ?? false;
                dto.IsUpCallBackRequested = upButton?.IsPressed ?? false;
            }

            return new FindEntitiesResponse<FloorDTO>(request) { Entities = dtos };
        }
    }
}