using IntrepidProducts.Shared.ElevatorSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.Repo
{
    public interface IElevatorRepository : IRepository<Elevator>
    {
        IEnumerable<Elevator> FindByBankId(Guid bankId);
    }

    public class ElevatorFileRepo : AbstractFileRepo<Elevator>, IElevatorRepository
    {
        public ElevatorFileRepo(RepoConfigurationManager configManager) : base(configManager)
        { }

        public IEnumerable<Elevator> FindByBankId(Guid bankId)
        {
            return Entities
                .Where(x => x.BankId == bankId);
        }

        public void Clear()
        {
            ClearAllEntities();
        }
    }
}