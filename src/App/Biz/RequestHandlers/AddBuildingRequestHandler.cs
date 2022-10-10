using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System;

namespace IntrepidProducts.Biz.RequestHandlers
{
    public class AddBuildingRequestHandler :
        AbstractRequestHandler<AddBuildingRequest, EntityOperationResponse>
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