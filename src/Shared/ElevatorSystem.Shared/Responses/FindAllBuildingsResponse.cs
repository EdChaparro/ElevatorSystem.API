using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.RequestResponse.Responses;
using System.Collections.Generic;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;

namespace IntrepidProducts.ElevatorSystem.Shared.Responses
{
    public class FindAllBanksResponse : ResponseAbstract
    {
        public FindAllBanksResponse(FindAllBanksRequest request) : base(request)
        {
            Banks = new List<BankDTO>();
        }

        public List<BankDTO> Banks { get; set; }
    }

    public class FindAllBuildingsResponse : ResponseAbstract
    {
        public FindAllBuildingsResponse(FindAllBuildingsRequest request) : base(request)
        {
            Buildings = new List<BuildingDTO>();
        }

        public List<BuildingDTO> Buildings { get; set; }
    }
}