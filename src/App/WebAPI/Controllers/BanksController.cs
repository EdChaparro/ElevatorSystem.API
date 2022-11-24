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
        [HttpGet]
        [ProducesResponseType(typeof(BankCollection), StatusCodes.Status200OK)]
        public ActionResult<Bank> Get(Guid buildingId)
        {
            var response = ProcessRequests<FindAllBanksRequest, FindEntityResponse<BankDTO>>
                    (new FindAllBanksRequest { BuildingId = buildingId })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            var banksResult = new BankCollection();
            foreach (var dto in response.Entities)
            {
                var bank = Bank.MapFrom(dto);
                bank.Link = GenerateActionByIdUri(nameof(Get), bank.Id);
                banksResult.Banks.Add(bank);
            }

            return Ok(banksResult);
        }
        #endregion

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([BindRequired, FromBody] Models.Bank? postBody)
        {
            if (postBody == null)
            {
                return BadRequest("Body empty, Bank object expected");
            }

            var response = ProcessRequests<AddBankRequest, EntityOperationResponse>
                (new AddBankRequest
                {
                    Bank = postBody.MapTo()

                })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            return CreatedAtAction(nameof(Get),
                new { id = postBody.BuildingId },
                postBody);
        }
    }
}