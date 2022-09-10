using System;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;

namespace IntrepidProducts.Biz.RequestHandlers
{
    public class AddBuildingRequestHandler :
        RequestHandlerAbstract<AddBuildingRequest, EntityAddedResponse>
    {
        protected override EntityAddedResponse DoHandle(AddBuildingRequest request)
        {
            return new EntityAddedResponse(request)
            {
                Id = Guid.NewGuid()   //TODO: Finish me
            };
        }
    }
}