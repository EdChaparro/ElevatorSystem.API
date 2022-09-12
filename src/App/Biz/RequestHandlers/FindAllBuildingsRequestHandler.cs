using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System.Linq;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;

namespace IntrepidProducts.Biz.RequestHandlers
{
    public class FindAllBuildingsRequestHandler :
        RequestHandlerAbstract<FindAllBuildingsRequest, FindAllBuildingsResponse>
    {
        public FindAllBuildingsRequestHandler(Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly Buildings _buildings;
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