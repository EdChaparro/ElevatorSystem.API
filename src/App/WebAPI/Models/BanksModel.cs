using System.Collections.Generic;
using System.Linq;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;

namespace IntrepidProducts.WebAPI.Models
{
    public class BanksModel
    {
        public List<Bank> Banks { get; set; }
            = new List<Bank>();

        public static BanksModel MapFrom(BanksDTO dto)
        {
            return new BanksModel
            {
                Banks = dto.Banks
                    .Select(Bank.MapFrom)
                    .ToList()
            };
        }
    }
}