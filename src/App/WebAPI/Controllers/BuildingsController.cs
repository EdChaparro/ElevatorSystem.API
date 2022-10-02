using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using IntrepidProducts.RequestResponse.Responses;

namespace IntrepidProducts.WebAPI.Controllers
{
    //TODO: HATEOAS - https://code-maze.com/hateoas-aspnet-core-web-api/

    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class BuildingsController : AbstractApiController
    {
        public BuildingsController
            (IRequestHandlerProcessor requestHandlerProcessor,
                LinkGenerator linkGenerator)
            : base(requestHandlerProcessor, linkGenerator)
        { }

        #region GET
        [HttpGet]
        [ProducesResponseType(typeof(BuildingsDTO), StatusCodes.Status200OK)]
        public ActionResult<BuildingsDTO> Get()
        {
            var response = ProcessRequests<FindAllBuildingsRequest, FindAllBuildingsResponse>
                    (new FindAllBuildingsRequest())
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            var buildings = new BuildingsModel();
            foreach (var dto in response.Buildings)
            {
                var building = Building.MapFrom(dto);
                building.Link = GenerateGetByIdUri(nameof(Get), building.Id);
                buildings.Buildings.Add(building);
            }

            return Ok(buildings);
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

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            if (response.Building == null)
            {
                return NotFound(id);
            }

            return Ok(response.Building);
        }
        #endregion

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] BuildingName? postBody)
        {
            if (postBody == null)
            {
                return BadRequest("Body empty, Building object expected");
            }

            var response = ProcessRequests<AddBuildingRequest, EntityOperationResponse>
                (new AddBuildingRequest
                {
                    Building = new BuildingDTO { Name = postBody.Name }

                })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            return CreatedAtAction(nameof(Get),
                new { id = response.EntityId },
                postBody);
        }

        [HttpPut("{id}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(Guid id, [FromBody] BuildingDTO? postBody)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Id");
            }

            if (postBody == null)
            {
                return BadRequest("Body empty, Building object expected");
            }

            var response = ProcessRequests<UpdateBuildingRequest, OperationResponse>
                (new UpdateBuildingRequest
                {
                    Building = new BuildingDTO { Id = id, Name = postBody.Name }

                })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            if (response.Result == OperationResult.NotFound)
            {
                return NotFound(id);
            }

            return NoContent();
        }
    }
}