using IntrepidProducts.ElevatorSystem.Banks;

namespace IntrepidProducts.Repo
{
    public class BankFileRepo : AbstractFileRepo<Bank>
    {
        public BankFileRepo(RepoConfigurationManager configManager) : base(configManager)
        { }
    }
}