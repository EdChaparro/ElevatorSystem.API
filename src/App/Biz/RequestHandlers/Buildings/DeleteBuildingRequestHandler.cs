using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System.Linq;

namespace IntrepidProducts.Biz.RequestHandlers.Buildings
{
    public class DeleteBuildingRequestHandler :
        AbstractRequestHandler<DeleteBuildingRequest, OperationResponse>
    {
        public DeleteBuildingRequestHandler(ElevatorSystem.Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly ElevatorSystem.Buildings _buildings;
        protected override OperationResponse DoHandle(DeleteBuildingRequest request)
        {
            var response = new OperationResponse(request) { Result = OperationResult.OperationalError };

            var building = _buildings.FirstOrDefault(x => x.Id == request.BuildingId);

            if (building == null)
            {
                response.Result = OperationResult.NotFound;
                return response;
            }

            var isDeleted = _buildings.Remove(building);

            if (isDeleted)
            {
                response.Result = OperationResult.Successful;
                return response;
            }

            return response;
        }
    }
}