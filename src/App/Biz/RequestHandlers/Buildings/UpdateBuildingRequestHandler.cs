using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System;

namespace IntrepidProducts.Biz.RequestHandlers.Buildings
{
    public class UpdateBuildingRequestHandler :
        AbstractRequestHandler<UpdateBuildingRequest, OperationResponse>
    {
        public UpdateBuildingRequestHandler(IRepository<Building> buildingRepo)
        {
            _buildingRepo = buildingRepo;
        }

        private readonly IRepository<Building> _buildingRepo;

        protected override OperationResponse DoHandle(UpdateBuildingRequest request)
        {
            var response = new OperationResponse(request);

            var buildingDTO = request.Building;
            if (buildingDTO == null)
            {
                throw new ArgumentException("Building object not provided");
            }

            IsValid(buildingDTO);   //Will generation error-response when invalid

            var building = _buildingRepo.FindById(buildingDTO.Id);

            if (building == null)
            {
                response.Result = OperationResult.NotFound;
                return response;
            }

            building.Name = buildingDTO.Name;
            var isSuccessful = _buildingRepo.Update(building) == 1;

            if (isSuccessful)
            {
                response.Result = OperationResult.Successful;
                return response;
            }

            throw new InvalidOperationException
                ($"Update for Building Id {building.Id} failed");
        }
    }
}