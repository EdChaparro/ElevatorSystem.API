using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using IntrepidProducts.WebAPI.Results;
using Building = IntrepidProducts.WebAPI.Models.Building;

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
        public ActionResult Get()
        {
            var response = ProcessRequests<FindAllBuildingsRequest, FindEntityResponse<BuildingDTO>>
                    (new FindAllBuildingsRequest())
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            var buildings = new Buildings();
            var links = new Links();

            foreach (var dto in response.Entities)
            {
                var building = Results.Building.MapFrom(dto);
                buildings.Add(building);
                links.Add(GenerateActionByIdUri(nameof(Get), building.Id, "Building"));
            }

            return Ok(new { Buildings = buildings, Links = links });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

            var building = Results.Building.MapFrom(response.Building);

            var banks = new Banks();

            foreach (var bankDTO in response.Banks)
            {
                var bank = Results.Bank.MapFrom(bankDTO);
                banks.Add(bank);
            }


            return Ok(new { Building = building, Banks = banks });
        }
        #endregion

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([BindRequired, FromBody] Building? postBody)
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(Guid id, [BindRequired, FromBody] Building? postBody)
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Id");
            }

            var response = ProcessRequests<DeleteBuildingRequest, OperationResponse>
                    (new DeleteBuildingRequest { BuildingId = id })
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