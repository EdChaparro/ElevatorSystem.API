using IntrepidProducts.ElevatorSystem;

namespace IntrepidProducts.Repo
{
    public class BuildingFileRepo : AbstractFileRepo<Building>, IFindAll<Building>
    {
        public BuildingFileRepo(RepoConfigurationManager configManager) : base(configManager)
        { }

        public void Clear()
        {
            ClearAllEntities();
        }
    }
}