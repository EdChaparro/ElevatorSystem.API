using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System.Linq;

namespace IntrepidProducts.Biz.RequestHandlers.Buildings
{
    public class FindAllBuildingsRequestHandler :
        AbstractRequestHandler<FindAllBuildingsRequest, FindEntityResponse<BuildingDTO>>
    {
        public FindAllBuildingsRequestHandler(IRepository<Building> buildingRepo)
        {
            _buildingRepo = buildingRepo;
        }

        private readonly IRepository<Building> _buildingRepo;

        protected override FindEntityResponse<BuildingDTO> DoHandle(FindAllBuildingsRequest request)
        {
            return new FindEntityResponse<BuildingDTO>(request)
            {
                Entities = _buildingRepo.FindAll().Select
                    (building => new BuildingDTO
                    {
                        Id = building.Id,
                        Name = building.Name
                    }).ToList()
            };
        }
    }
}