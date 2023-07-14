using IntrepidProducts.Shared.ElevatorSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.Repo
{
    public interface IBuildingElevatorBankRepository : IRepository<BuildingElevatorBank>
    {
        IEnumerable<BuildingElevatorBank> FindByBuildingId(Guid buildingId);
    }

    public class BankFileRepo : AbstractFileRepo<BuildingElevatorBank>, IBuildingElevatorBankRepository
    {
        public BankFileRepo(RepoConfigurationManager configManager) : base(configManager)
        { }

        public IEnumerable<BuildingElevatorBank> FindByBuildingId(Guid buildingId)
        {
            return Entities
                .Where(x => x.BuildingId == buildingId);
        }

        public void Clear()
        {
            ClearAllEntities();
        }
    }
}