using IntrepidProducts.ElevatorSystem.Shared.DTOs.Elevators;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Elevators;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.WebAPI.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace IntrepidProducts.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/buildings/{buildingId}/banks/{bankId}/[controller]")]
    [Produces("application/json")]
    public class ElevatorsController : AbstractApiController
    {
        public ElevatorsController
        (IRequestHandlerProcessor requestHandlerProcessor,
            LinkGenerator linkGenerator)
            : base(requestHandlerProcessor, linkGenerator)
        { }

        #region GET
        [HttpGet]
        public ActionResult Get(Guid buildingId, Guid bankId)
        {
            var response = ProcessRequests<FindAllElevatorsRequest, FindEntitiesResponse<ElevatorDTO>>
                    (new FindAllElevatorsRequest { BankId = bankId })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            var elevators = new Elevators();
            var links = new Links();

            foreach (var dto in response.Entities)
            {
                var elevator = Results.Elevator.MapFrom(dto);
                elevators.Add(elevator);
                links.Add(GenerateActionByIdUri(nameof(Get), elevator.Id, "Elevator"));
            }

            return Ok(new { Elevators = elevators, Links = links });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid buildingId, Guid bankId, Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Id");
            }

            var response = ProcessRequests<FindElevatorRequest, FindEntityResponse<ElevatorDTO>>
                    (new FindElevatorRequest { BankId = bankId, ElevatorId = id })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            if (response.Entity == null)
            {
                return NotFound(id);
            }

            var elevator = Results.Elevator.MapFrom(response.Entity);

            return Ok(new { Elevator = elevator });
        }

        #endregion
    }
}