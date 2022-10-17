using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.WebAPI.Results;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.WebAPI.Models
{
    public class BankCollection
    {
        public List<Bank> Banks { get; set; }
            = new List<Bank>();

        public static BankCollection MapFrom(BanksDTO dto)
        {
            return new BankCollection
            {
                Banks = dto.Banks
                    .Select(Bank.MapFrom)
                    .ToList()
            };
        }
    }
}