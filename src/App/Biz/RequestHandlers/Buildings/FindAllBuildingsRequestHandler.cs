using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System.Linq;

namespace IntrepidProducts.Biz.RequestHandlers.Buildings
{
    public class FindAllBuildingsRequestHandler :
        AbstractRequestHandler<FindAllBuildingsRequest, FindAllBuildingsResponse>
    {
        public FindAllBuildingsRequestHandler(ElevatorSystem.Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly ElevatorSystem.Buildings _buildings;
        protected override FindAllBuildingsResponse DoHandle(FindAllBuildingsRequest request)
        {
            return new FindAllBuildingsResponse(request)
            {
                Buildings = _buildings.Select
                    (building => new BuildingDTO
                    {
                        Id = building.Id,
                        Name = building.Name
                    }).ToList()
            };
        }
    }
}