using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;

namespace IntrepidProducts.Biz.RequestHandlers.Buildings
{
    public class DeleteBuildingRequestHandler :
        AbstractRequestHandler<DeleteBuildingRequest, OperationResponse>
    {
        public DeleteBuildingRequestHandler(IRepository<Building> buildingRepo)
        {
            _buildingRepo = buildingRepo;
        }

        private readonly IRepository<Building> _buildingRepo;

        protected override OperationResponse DoHandle(DeleteBuildingRequest request)
        {
            var response = new OperationResponse(request) { Result = OperationResult.OperationalError };

            var building = _buildingRepo.FindById(request.BuildingId);

            if (building == null)
            {
                response.Result = OperationResult.NotFound;
                return response;
            }

            var isDeleted = _buildingRepo.Delete(building) == 1;

            if (isDeleted)
            {
                response.Result = OperationResult.Successful;
                return response;
            }

            return response;
        }
    }
}