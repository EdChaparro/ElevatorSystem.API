using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IntrepidProducts.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/building/{buildingId}/[controller]")]
    [Produces("application/json")]
    public class BanksController : AbstractApiController
    {
        public BanksController
        (IRequestHandlerProcessor requestHandlerProcessor,
            LinkGenerator linkGenerator)
            : base(requestHandlerProcessor, linkGenerator)
        {
        }
    }
}