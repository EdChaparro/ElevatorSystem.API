using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System;
using System.Linq;

namespace IntrepidProducts.Biz.RequestHandlers.Buildings
{
    public class UpdateBuildingRequestHandler :
        AbstractRequestHandler<UpdateBuildingRequest, OperationResponse>
    {
        public UpdateBuildingRequestHandler(ElevatorSystem.Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly ElevatorSystem.Buildings _buildings;
        protected override OperationResponse DoHandle(UpdateBuildingRequest request)
        {
            var response = new OperationResponse(request);

            var buildingDTO = request.Building;
            if (buildingDTO == null)
            {
                throw new ArgumentException("Building object not provided");
            }

            IsValid(buildingDTO);   //Will generation error-response when invalid

            var building = _buildings
                .FirstOrDefault(x => x.Id == buildingDTO.Id);

            if (building == null)
            {
                response.Result = OperationResult.NotFound;
                return response;
            }

            building.Name = buildingDTO.Name;
            response.Result = OperationResult.Successful;

            return response;
        }
    }
}