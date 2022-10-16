using System;
using System.Collections.Generic;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System.Linq;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;

namespace IntrepidProducts.Biz.RequestHandlers.Banks
{
    public class FindAllBanksRequestHandler :
        AbstractRequestHandler<FindAllBanksRequest, FindAllBanksResponse>
    {
        public FindAllBanksRequestHandler(ElevatorSystem.Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly ElevatorSystem.Buildings _buildings;
        protected override FindAllBanksResponse DoHandle(FindAllBanksRequest request)
        {
            var building = _buildings.FirstOrDefault(x => x.Id == request.BuildingId);

            if (building == null)
            {
                return new FindAllBanksResponse(request);
            }

            var bankDTOs = Map(building.Id, building.Banks);

            return new FindAllBanksResponse(request) { Banks = bankDTOs };

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