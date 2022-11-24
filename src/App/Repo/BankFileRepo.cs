using System;
using System.Collections.Generic;
using System.Linq;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.Repo
{
    public interface IFindByBusinessId
    {
        public IEnumerable<BuildingElevatorBank> FindByBusinessId(Guid businessId);
    }

    public class BankFileRepo : AbstractFileRepo<BuildingElevatorBank>, IFindByBusinessId
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