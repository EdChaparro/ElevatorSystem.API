using IntrepidProducts.Biz.Mappers;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Elevators;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Elevators;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;

namespace IntrepidProducts.Biz.RequestHandlers.Elevators
{
    public class FindAllElevatorsRequestHandler :
        AbstractRequestHandler<FindAllElevatorsRequest, FindEntityResponse<ElevatorDTO>>
    {
        public FindAllElevatorsRequestHandler(IBuildingElevatorBankRepository bankRepo)
        {
            _bankRepo = bankRepo;
        }

        private readonly IBuildingElevatorBankRepository _bankRepo;

        protected override FindEntityResponse<ElevatorDTO> DoHandle(FindAllElevatorsRequest request)
        {
            var bank = _bankRepo.FindById(request.BankId);

            if (bank == null)
            {
                return new FindEntityResponse<ElevatorDTO>(request);
            }

            var elevatorDTOs = ElevatorMapper.Map(bank.Elevators);

            return new FindEntityResponse<ElevatorDTO>(request) { Entities = elevatorDTOs };
        }
    }
}