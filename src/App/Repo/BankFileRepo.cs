using IntrepidProducts.Shared.ElevatorSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.Repo
{
    public interface IBuildingElevatorBankRepository : IRepository<BuildingElevatorBank>
    {
        IEnumerable<BuildingElevatorBank> FindByBusinessId(Guid businessId);
    }

    public class BankFileRepo : AbstractFileRepo<BuildingElevatorBank>, IBuildingElevatorBankRepository
    {
        public BankFileRepo(RepoConfigurationManager configManager) : base(configManager)
        { }

        public IEnumerable<BuildingElevatorBank> FindByBusinessId(Guid businessId)
        {
            return Entities
                .Where(x => x.BuildingId == businessId);
        }

        public void Clear()
        {
            ClearAllEntities();
        }
    }
}