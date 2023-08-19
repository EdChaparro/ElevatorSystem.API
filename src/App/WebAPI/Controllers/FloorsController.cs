using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks.Floors;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks.Floors;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.WebAPI.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace IntrepidProducts.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/buildings/{buildingId}/banks/{bankId}/[controller]")]
    [Produces("application/json")]
    public class FloorsController : AbstractApiController
    {
        public FloorsController
        (IRequestHandlerProcessor requestHandlerProcessor,
            LinkGenerator linkGenerator)
            : base(requestHandlerProcessor, linkGenerator)
        {
        }

        #region GET

        [HttpGet]
        public ActionResult Get(Guid buildingId, Guid bankId)
        {
            var response = ProcessRequests<FindAllFloorsRequest, FindEntitiesResponse<FloorDTO>>
                    (new FindAllFloorsRequest
                    {
                        BuildingId = buildingId,
                        BankId = bankId
                    })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            var floors = new Floors();
            var links = new Links();

            foreach (var dto in response.Entities)
            {
                var floor = Floor.MapFrom(dto);
                floors.Add(floor);
            }

            return Ok(new { Floors = floors, Links = links });

        }

        #endregion
    }
}