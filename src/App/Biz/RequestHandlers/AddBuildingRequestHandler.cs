using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System;
using IntrepidProducts.RequestResponse.Responses;

namespace IntrepidProducts.Biz.RequestHandlers
{
    public class AddBuildingRequestHandler :
        RequestHandlerAbstract<AddBuildingRequest, EntityOperationResponse>
    {
        public AddBuildingRequestHandler(Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly Buildings _buildings;
        protected override EntityOperationResponse DoHandle(AddBuildingRequest request)
        {
            var buildingDTO = request.Building;
            if (buildingDTO == null)
            {
                throw new ArgumentException("Building object not provided");
            }

            if (string.IsNullOrWhiteSpace(buildingDTO.Name))
            {
                throw new ArgumentException("Building name is required");
            }

            var building = new Building
            {
                Name = buildingDTO.Name
            };

            _buildings.Add(building);


            return new EntityOperationResponse(request)
            {
                EntityId = building.Id
            };
        }
    }
}