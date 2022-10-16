using System.Collections.Generic;

namespace IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks
{
    public class BanksDTO : IDataTransferObject
    {
        public List<BankDTO> Banks { get; set; }
            = new List<BankDTO>();
    }
}