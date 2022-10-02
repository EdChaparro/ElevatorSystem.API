using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System.Linq;
using IntrepidProducts.RequestResponse.Responses;

namespace IntrepidProducts.Biz.RequestHandlers
{
    public class DeleteBuildingRequestHandler :
        RequestHandlerAbstract<DeleteBuildingRequest, OperationResponse>
    {
        public DeleteBuildingRequestHandler(Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly Buildings _buildings;
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