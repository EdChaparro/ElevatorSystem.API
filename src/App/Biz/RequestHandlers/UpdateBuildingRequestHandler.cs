using System;
using System.Linq;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;

namespace IntrepidProducts.Biz.RequestHandlers
{
    public class UpdateBuildingRequestHandler :
        RequestHandlerAbstract<UpdateBuildingRequest, OperationResponse>
    {
        public UpdateBuildingRequestHandler(Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly Buildings _buildings;
        protected override OperationResponse DoHandle(UpdateBuildingRequest request)
        {
            var response = new OperationResponse(request);

            var buildingDTO = request.Building;
            if (buildingDTO == null)
            {
                throw new ArgumentException("Building object not provided");
            }

            if (string.IsNullOrWhiteSpace(buildingDTO.Name))
            {
                throw new ArgumentException("Building name is required");
            }

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