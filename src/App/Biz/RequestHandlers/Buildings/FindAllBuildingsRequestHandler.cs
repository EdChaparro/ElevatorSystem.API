using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System.Linq;

namespace IntrepidProducts.Biz.RequestHandlers.Buildings
{
    public class FindAllBuildingsRequestHandler :
        AbstractRequestHandler<FindAllBuildingsRequest, FindEntityResponse<BuildingDTO>>
    {
        public FindAllBuildingsRequestHandler(ElevatorSystem.Buildings buildings)
        {
            _buildings = buildings; //Singleton
        }

        private readonly ElevatorSystem.Buildings _buildings;
        protected override FindEntityResponse<BuildingDTO> DoHandle(FindAllBuildingsRequest request)
        {
            return new FindEntityResponse<BuildingDTO>(request)
            {
                Entities = _buildings.Select
                    (building => new BuildingDTO
                    {
                        Id = building.Id,
                        Name = building.Name
                    }).ToList()
            };
        }
    }
}