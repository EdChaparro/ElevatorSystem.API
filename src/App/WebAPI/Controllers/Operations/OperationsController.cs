using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.RequestResponse.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.AspNetCore.Routing;

namespace IntrepidProducts.WebAPI.Controllers.Operations
{
    [ApiController]
    [Route("api/v1/buildings/{buildingId}/{bankId}/[controller]")]
    [Produces("application/json")]
    public class OperationsController : AbstractApiController
    {
        public OperationsController
            (IRequestHandlerProcessor requestHandlerProcessor,
                LinkGenerator linkGenerator)
                : base(requestHandlerProcessor, linkGenerator)
        { }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(Guid buildingId, Guid bankId, [AsParameters] string engine)
        {
            const string ENGINE_START = "Start";
            const string ENGINE_STOP = "Stop";

            if (buildingId == Guid.Empty)
            {
                return BadRequest("Invalid Building Id");
            }

            if (bankId == Guid.Empty)
            {
                return BadRequest("Invalid Bank Id");
            }

            switch (engine)
            {
                case ENGINE_START:
                    return StartBankService(buildingId, bankId);

                case ENGINE_STOP:
                    return StopBankService(buildingId, bankId);

                default:
                    return BadRequest("Engine Parameter is invalid");
            }
        }

        private StatusCodeResult StartBankService(Guid buildingId, Guid bankId)
        {

            return NoContent(); //TODO: Finish Me
        }

        private StatusCodeResult StopBankService(Guid buildingId, Guid bankId)
        {

            return NoContent(); //TODO: Finish Me
        }


    }
}
