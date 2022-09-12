using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System.Linq;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;

namespace IntrepidProducts.Biz.RequestHandlers
{
    public class FindBuildingRequestHandler :
        RequestHandlerAbstract<FindBuildingRequest, FindBuildingResponse>
    {
        public FindBuildingRequestHandler(Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly Buildings _buildings;
        protected override FindBuildingResponse DoHandle(FindBuildingRequest request)
        {
            var building = _buildings.FirstOrDefault(x => x.Id == request.BuildingId);

            return new FindBuildingResponse(request)
            {
                Building = new BuildingDTO { Name = building?.Name }
            };
        }
    }
}