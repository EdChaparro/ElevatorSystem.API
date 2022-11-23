using IntrepidProducts.Common;
using IntrepidProducts.Repo;

namespace RepoTest
{
    public class TestEntity : AbstractEntity
    {
        public string? Value1 { get; set; }
        public string? Value2 { get; set; }
    }

    public class TestEntityFileRepo : AbstractFileRepo<TestEntity>, IFindAll<TestEntity>, IClear
    {
        public TestEntityFileRepo(RepoConfigurationManager configManager) : base(configManager)
        { }

        public void Clear()
        {
            ClearAllEntities();
        }
    }
}