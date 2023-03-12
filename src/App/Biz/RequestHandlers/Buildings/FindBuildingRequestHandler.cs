using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.ElevatorSystemBiz.Mappers;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;

namespace IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Buildings
{
    public class FindBuildingRequestHandler :
        AbstractRequestHandler<FindBuildingRequest, FindBuildingResponse>
    {
        public FindBuildingRequestHandler
            (IRepository<Building> buildingRepo, IBuildingElevatorBankRepository bankRepo)
        {
            _buildingRepo = buildingRepo;
            _bankRepo = bankRepo;
        }

        private readonly IRepository<Building> _buildingRepo;
        private readonly IBuildingElevatorBankRepository _bankRepo;

        protected override FindBuildingResponse DoHandle(FindBuildingRequest request)
        {
            var building = _buildingRepo.FindById(request.BuildingId);

            var dto = building == null
                ? null
                : new BuildingDTO
                {
                    Id = building.Id,
                    Name = building?.Name
                };

            var banks = _bankRepo.FindByBusinessId(request.BuildingId);

            var bankDTOs = BankMapper.Map(banks);

            return new FindBuildingResponse(request)
            {
                Building = dto,
                Banks = bankDTOs
            };
        }
    }
}