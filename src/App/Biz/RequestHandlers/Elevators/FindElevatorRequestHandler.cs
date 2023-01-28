using IntrepidProducts.Biz.Mappers;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Elevators;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Elevators;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using System.Linq;

namespace IntrepidProducts.Biz.RequestHandlers.Elevators
{
    public class FindElevatorRequestHandler :
        AbstractRequestHandler<FindElevatorRequest, FindEntityResponse<ElevatorDTO>>
    {
        public FindElevatorRequestHandler(IRepository<BuildingElevatorBank> bankRepo)
        {
            _bankRepo = bankRepo;
        }

        private readonly IRepository<BuildingElevatorBank> _bankRepo;

        protected override FindEntityResponse<ElevatorDTO> DoHandle(FindElevatorRequest request)
        {
            var bank = _bankRepo.FindById(request.BankId);

            if (bank == null)
            {
                return new FindEntityResponse<ElevatorDTO>(request);
            }

            var elevatorDTO = ElevatorMapper
                .Map(bank.Elevators
                    .FirstOrDefault(x => x.Id == request.ElevatorId));

            return new FindEntityResponse<ElevatorDTO>(request) { Entity = elevatorDTO };
        }
    }
}