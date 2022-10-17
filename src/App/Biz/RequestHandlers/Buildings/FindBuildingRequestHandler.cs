using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System.Linq;

namespace IntrepidProducts.Biz.RequestHandlers.Buildings
{
    public class FindBuildingRequestHandler :
        AbstractRequestHandler<FindBuildingRequest, FindBuildingResponse>
    {
        public FindBuildingRequestHandler(ElevatorSystem.Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly ElevatorSystem.Buildings _buildings;
        protected override FindBuildingResponse DoHandle(FindBuildingRequest request)
        {
            var building = _buildings.FirstOrDefault(x => x.Id == request.BuildingId);

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