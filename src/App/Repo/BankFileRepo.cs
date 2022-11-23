using System;
using System.Collections.Generic;
using System.Linq;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.Repo
{
    public interface IFindByBusinessId
    {
        public IEnumerable<Bank> FindByBusinessId(Guid businessId);
    }

    public class BankFileRepo : AbstractFileRepo<BuildingElevatorBank>, IFindByBusinessId
    {
        public BankFileRepo(RepoConfigurationManager configManager) : base(configManager)
        { }

        public IEnumerable<Bank> FindByBusinessId(Guid businessId)
        {
            return Entities
                .Where(x => x.BuildingId == businessId)
                .Select(x => x.Bank);
        }

        public void Clear()
        {
            ClearAllEntities();
        }
    }
}