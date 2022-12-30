using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.WebAPI.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace IntrepidProducts.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/buildings/{buildingId}/[controller]")]
    [Produces("application/json")]
    public class BanksController : AbstractApiController
    {
        public BanksController
        (IRequestHandlerProcessor requestHandlerProcessor,
            LinkGenerator linkGenerator)
            : base(requestHandlerProcessor, linkGenerator)
        { }

        #region GET
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid buildingId, Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Id");
            }

            var response = ProcessRequests<FindBankRequest, FindBankResponse>
                    (new FindBankRequest { BankId = id })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            if (response.Bank == null)
            {
                return NotFound(id);
            }

            return Ok(response.Bank);
        }

        [HttpGet]
        public ActionResult Get(Guid buildingId)
        {
            var response = ProcessRequests<FindAllBanksRequest, FindEntityResponse<BankDTO>>
                    (new FindAllBanksRequest { BuildingId = buildingId })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            var banks = new Banks();
            var links = new Links();

            foreach (var dto in response.Entities)
            {
                var bank = Results.Bank.MapFrom(dto);
                banks.Add(bank);
                links.Add(GenerateActionByIdUri(nameof(Get), bank.Id, "Bank"));
            }

            return Ok(new { Banks = banks, Links = links });

        }
        #endregion

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(Guid buildingId, [BindRequired, FromBody] Models.Bank? postBody)
        {
            if (postBody == null)
            {
                return BadRequest("Body empty, Bank object expected");
            }

            var bank = postBody.MapTo();
            bank.BuildingId = buildingId;

            var response = ProcessRequests<AddBankRequest, EntityOperationResponse>
                    (new AddBankRequest { Bank = bank })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            return CreatedAtAction(nameof(Get),
                new { buildingId = buildingId, id = response.EntityId },
                postBody);
        }
    }
}