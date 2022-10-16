using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(typeof(BuildingsDTO), StatusCodes.Status200OK)]
        public ActionResult<BankDTO> Get(Guid buildingId)
        {
            var response = ProcessRequests<FindAllBanksRequest, FindEntityResponse<BankDTO>>
                    (new FindAllBanksRequest { BuildingId = buildingId })
                .First();

            if (!response.IsSuccessful)
            {
                return GetProblemDetails(response);
            }

            var banksModel = new BanksModel();
            foreach (var dto in response.Entities)
            {
                var bank = Bank.MapFrom(dto);
                bank.Link = GenerateGetByIdUri(nameof(Get), bank.Id);
                banksModel.Banks.Add(bank);
            }

            return Ok(banksModel);
        }
        #endregion
    }
}