using System;
using System.Linq;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponse.Requests;
using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntrepidProducts.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class BuildingsController : ControllerBase
    {
        public BuildingsController(IRequestHandlerProcessor requestHandlerProcessor)
        {
            _requestHandlerProcessor = requestHandlerProcessor;
        }

        private readonly IRequestHandlerProcessor _requestHandlerProcessor;

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BuildingDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Id");
            }

            return Ok(new BuildingDTO());   //TODO: Finish me
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] BuildingDTO? buildingsDTO)
        {
            if (buildingsDTO == null)
            {
                return BadRequest("Body empty, Building object expected");
            }

            var requestBlock = new RequestBlock();
            var request = new AddBuildingRequest
            {
                Building = buildingsDTO
            };

            requestBlock.Add(request);

            var responseBlock
                = _requestHandlerProcessor.Process(requestBlock);

            if (responseBlock.HasErrors)
            {
                Problem("Persistence error", null,
                    StatusCodes.Status500InternalServerError);
            }

            var response = (EntityAddedResponse)responseBlock.Responses.First();

            if (!response.IsSuccessful)
            {
                return Problem("Unexpected Response from Request Handler", null,
                    StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(Get),
                new { id = response.EntityId },
                request.Building);
        }
    }
}