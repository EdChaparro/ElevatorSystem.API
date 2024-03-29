﻿using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using System.Linq;

namespace IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Buildings
{
    public class FindAllBuildingsRequestHandler :
        AbstractRequestHandler<FindAllBuildingsRequest, FindEntitiesResponse<BuildingDTO>>
    {
        public FindAllBuildingsRequestHandler(IRepository<Building> buildingRepo)
        {
            _buildingRepo = buildingRepo;
        }

        private readonly IRepository<Building> _buildingRepo;

        protected override FindEntitiesResponse<BuildingDTO> DoHandle(FindAllBuildingsRequest request)
        {
            return new FindEntitiesResponse<BuildingDTO>(request)
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