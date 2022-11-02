using IntrepidProducts.ElevatorSystem;
using System.Collections.Generic;

namespace IntrepidProducts.Repo
{
    public class BuildingRepo : AbstractFileRepo<Building>, IFindAll<Building>
    {
        public BuildingRepo(RepoConfigurationManager configManager) : base(configManager)
        { }

        public IEnumerable<Building> FindAll()
        {
            return FindAllEntities();
        }
    }
}