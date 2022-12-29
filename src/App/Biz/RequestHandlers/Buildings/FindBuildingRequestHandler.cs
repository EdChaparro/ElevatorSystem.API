using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.Biz.RequestHandlers.Buildings
{
    public class FindBuildingRequestHandler :
        AbstractRequestHandler<FindBuildingRequest, FindBuildingResponse>
    {
        public FindBuildingRequestHandler
            (IRepository<Building> buildingRepo, IRepository<BuildingElevatorBank> bankRepo)
        {
            _buildingRepo = buildingRepo;
            _bankRepo = bankRepo;
        }

        private readonly IRepository<Building> _buildingRepo;
        private readonly IRepository<BuildingElevatorBank> _bankRepo;

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

            return new FindBuildingResponse(request)
            {
                Building = dto
            };
        }
    }
}