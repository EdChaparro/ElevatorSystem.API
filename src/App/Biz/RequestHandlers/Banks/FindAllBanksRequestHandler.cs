using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.Biz.RequestHandlers.Banks
{
    public class FindAllBanksRequestHandler :
        AbstractRequestHandler<FindAllBanksRequest, FindEntityResponse<BankDTO>>
    {
        public FindAllBanksRequestHandler(IRepository<BuildingElevatorBank> bankRepo)
        {
            _bankRepo = bankRepo;
        }

        private readonly IRepository<BuildingElevatorBank> _bankRepo;

        protected override FindEntityResponse<BankDTO> DoHandle(FindAllBanksRequest request)
        {
            //TODO: Hacky, find a better way to do this
            var repo = _bankRepo as IFindByBusinessId;

            if (repo == null)
            {
                throw new NullReferenceException
                    ("Bank Repo does not support IFindByBusinessId interface");
            }

            var banks = repo.FindByBusinessId(request.BuildingId);

            var bankDTOs = Map(request.BuildingId, banks);

            return new FindEntityResponse<BankDTO>(request) { Entities = bankDTOs };
        }

        private static List<BankDTO> Map(Guid buildingId, IEnumerable<IBank> buildingBanks)
        {
            var banks = new List<BankDTO>();

            foreach (var bank in buildingBanks)
            {
                var dto = new BankDTO
                {
                    BuildingId = buildingId,
                    Id = bank.Id,
                    Name = bank.Name,
                    FloorNbrs = bank.OrderedFloorNumbers.ToList(),
                    HighestFloorNbr = bank.HighestFloorNbr,
                    LowestFloorNbr = bank.LowestFloorNbr,
                    NumberOfElevators = bank.NumberOfElevators
                };

                banks.Add(dto);
            }

            return banks;
        }
    }
}