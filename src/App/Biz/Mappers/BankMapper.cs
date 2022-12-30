using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using System.Collections.Generic;

namespace IntrepidProducts.Biz.Mappers
{
    public static class BankMapper
    {
        internal static List<BankDTO> Map(IEnumerable<BuildingElevatorBank> buildingBanks)
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
