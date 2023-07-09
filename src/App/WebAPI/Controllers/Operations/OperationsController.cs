using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

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
            const string ENGINE_START = "start";
            const string ENGINE_STOP = "stop";

            if (buildingId == Guid.Empty)
            {
                return BadRequest("Invalid Building Id");
            }

            if (bankId == Guid.Empty)
            {
                return BadRequest("Invalid Bank Id");
            }

            if (string.IsNullOrEmpty(engine))
            {
                return BadRequest("Engine Parameter is invalid");
            }

            var engineQueryParameter = engine.ToLower();

            switch (engineQueryParameter)
            {
                case ENGINE_START:
                    return StartBankService(buildingId, bankId);

                case ENGINE_STOP:
                    return StopBankService(buildingId, bankId);

                default:
                    return BadRequest("Engine Parameter is invalid");
            }
        }

        private IActionResult StartBankService(Guid buildingId, Guid bankId)
        {
            var response = ProcessRequests<StartBankRequest, BankOperationsResponse>
                    (new StartBankRequest
                    {
                        BusinessId = buildingId,
                        BankId = bankId
                    })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            return Ok();
        }

        private IActionResult StopBankService(Guid buildingId, Guid bankId)
        {
            var response = ProcessRequests<StopBankRequest, BankOperationsResponse>
                (new StopBankRequest
                {
                    BusinessId = buildingId,
                    BankId = bankId
                })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            return Ok();
        }
    }
}