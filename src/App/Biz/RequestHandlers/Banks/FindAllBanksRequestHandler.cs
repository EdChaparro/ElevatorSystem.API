using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using System.Collections.Generic;

namespace IntrepidProducts.Biz.RequestHandlers.Banks
{
    public class FindAllBanksRequestHandler :
        AbstractRequestHandler<FindAllBanksRequest, FindEntityResponse<BankDTO>>
    {
        public FindAllBanksRequestHandler(IBuildingElevatorBankRepository bankRepo)
        {
            _bankRepo = bankRepo;
        }

        private readonly IBuildingElevatorBankRepository _bankRepo;

        protected override FindEntityResponse<BankDTO> DoHandle(FindAllBanksRequest request)
        {
            var banks = _bankRepo.FindByBusinessId(request.BuildingId);

            var bankDTOs = Map(banks);

            return new FindEntityResponse<BankDTO>(request) { Entities = bankDTOs };
        }

        private static List<BankDTO> Map(IEnumerable<BuildingElevatorBank> buildingBanks)
        {
            var banks = new List<BankDTO>();

            foreach (var bank in buildingBanks)
            {
                var dto = new BankDTO
                {
                    BuildingId = bank.BuildingId,
                    Id = bank.Id,
                    Name = bank.Name,
                    FloorNbrs = bank.FloorNbrs,
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