using System;
using IntrepidProducts.Common.Requests;
using IntrepidProducts.Common.Responses;
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