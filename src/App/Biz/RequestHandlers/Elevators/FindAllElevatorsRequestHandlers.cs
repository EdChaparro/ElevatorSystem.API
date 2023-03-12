using IntrepidProducts.ElevatorSystem.Shared.DTOs.Elevators;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Elevators;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.ElevatorSystemBiz.Mappers;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Elevators
{
    public class FindAllElevatorsRequestHandler :
        AbstractRequestHandler<FindAllElevatorsRequest, FindEntitiesResponse<ElevatorDTO>>
    {
        public FindAllElevatorsRequestHandler(IRepository<BuildingElevatorBank> bankRepo)
        {
            _bankRepo = bankRepo;
        }

        private readonly IRepository<BuildingElevatorBank> _bankRepo;

        protected override FindEntitiesResponse<ElevatorDTO> DoHandle(FindAllElevatorsRequest request)
        {
            var bank = _bankRepo.FindById(request.BankId);

            if (bank == null)
            {
                return new FindEntitiesResponse<ElevatorDTO>(request);
            }

            var elevatorDTOs = ElevatorMapper.Map(bank.Elevators);

            return new FindEntitiesResponse<ElevatorDTO>(request) { Entities = elevatorDTOs };
        }
    }
}