using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System;

namespace IntrepidProducts.Biz.RequestHandlers
{
    public class AddBuildingRequestHandler :
        RequestHandlerAbstract<AddBuildingRequest, EntityAddedResponse>
    {
        public AddBuildingRequestHandler(Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly Buildings _buildings;
        protected override EntityAddedResponse DoHandle(AddBuildingRequest request)
        {
            var buildingDTO = request.Building;
            if (buildingDTO == null)
            {
                throw new ArgumentException("Building object not provided");
            }

            var building = new Building
            {
                Name = buildingDTO.Name
            };

            _buildings.Add(building);


            return new EntityAddedResponse(request)
            {
                EntityId = building.Id
            };
        }
    }
}