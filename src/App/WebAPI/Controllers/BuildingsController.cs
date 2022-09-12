using System;
using System.Collections.Generic;
using System.Linq;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntrepidProducts.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class BuildingsController : AbstractApiController
    {
        public BuildingsController(IRequestHandlerProcessor requestHandlerProcessor)
        : base(requestHandlerProcessor)
        { }

        [HttpGet]
        public ActionResult<IEnumerable<BuildingDTO>> Get()
        {
            var response = ProcessRequests<FindAllBuildingsRequest, FindAllBuildingsResponse>
                    (new FindAllBuildingsRequest())
                .First();

            return Ok(response.Buildings);
        }


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

            var response = ProcessRequests<FindBuildingRequest, FindBuildingResponse>
                    (new FindBuildingRequest { BuildingId = id })
                .First();

            if (response.Building == null)
            {
                return NotFound(id);
            }

            return Ok(response.Building);
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

            var response = ProcessRequests<AddBuildingRequest, EntityAddedResponse>
                    (new AddBuildingRequest { Building = buildingsDTO })
                .First();

            return CreatedAtAction(nameof(Get),
                new { id = response.EntityId },
                buildingsDTO);
        }
    }
}