using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System;

namespace IntrepidProducts.Biz.RequestHandlers.Buildings
{
    public class AddBuildingRequestHandler :
        AbstractRequestHandler<AddBuildingRequest, EntityOperationResponse>
    {
        public AddBuildingRequestHandler(IRepository<Building> buildingRepo)
        {
            _buildingRepo = buildingRepo;
        }

        private readonly IRepository<Building> _buildingRepo;
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

            _buildingRepo.Create(building);


            return new EntityOperationResponse(request)
            {
                EntityId = building.Id
            };
        }
    }
}