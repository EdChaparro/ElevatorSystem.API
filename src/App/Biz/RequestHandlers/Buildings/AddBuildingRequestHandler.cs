using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System;

namespace IntrepidProducts.Biz.RequestHandlers.Buildings
{
    public class AddBuildingRequestHandler :
        AbstractRequestHandler<AddBuildingRequest, EntityOperationResponse>
    {
        public AddBuildingRequestHandler(ElevatorSystem.Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly ElevatorSystem.Buildings _buildings;
        protected override EntityOperationResponse DoHandle(AddBuildingRequest request)
        {
            var buildingDTO = request.Building;
            if (buildingDTO == null)
            {
                throw new ArgumentException("Building object not provided");
            }

            IsValid(buildingDTO);   //Will generation error-response when invalid

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