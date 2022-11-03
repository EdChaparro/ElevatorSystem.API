using System;
using System.Linq;
using IntrepidProducts.Common;
using IntrepidProducts.Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RepoTest
{
    #region Test Classes to Exercise Abstract Class Code
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
    #endregion

    [TestClass]
    public class AbstractFileRepoTest
    {
        private TestEntityFileRepo _repo;

        [TestInitialize]
        public void Initialize()
        {
            var configManager = new RepoConfigurationManager("TestDb");
            _repo = new TestEntityFileRepo(configManager);
            _repo.Clear();  //Clear all data
        }

        [TestMethod]
        public void ShouldUseEntityNameAsFileName()
        {
            var expectedFileName = nameof(TestEntity) + ".json";
            Assert.AreEqual(expectedFileName,
                _repo.FilePath
                    .Substring(_repo.FilePath.Length - expectedFileName.Length));
        }

        #region Create

        [TestMethod]
        public void ShouldPersistEntity()
        {
            var entity = new TestEntity { Value1 = "Foo", Value2 = "Bar" };

            Assert.AreEqual(1, _repo.Create(entity));

            var deserializedEntity = _repo.FindById(entity.Id);

            Assert.IsNotNull(deserializedEntity);
            Assert.AreEqual(entity.Id, deserializedEntity.Id);
            Assert.AreEqual(entity.Value1, deserializedEntity.Value1);
            Assert.AreEqual(entity.Value2, deserializedEntity.Value2);
        }

        [TestMethod]
        public void ShouldPersistMultipleEntities()
        {
            var entity1 = new TestEntity { Value1 = "Foo", Value2 = "Bar" };
            var entity2 = new TestEntity { Value1 = "Vice", Value2 = "Roy" };

            Assert.AreEqual(1, _repo.Create(entity1));
            Assert.AreEqual(1, _repo.Create(entity2));

            var deserializedEntity1 = _repo.FindById(entity1.Id);
            var deserializedEntity2 = _repo.FindById(entity2.Id);

            Assert.IsNotNull(deserializedEntity1);
            Assert.AreEqual(entity1.Id, deserializedEntity1.Id);
            Assert.AreEqual(entity1.Value1, deserializedEntity1.Value1);
            Assert.AreEqual(entity1.Value2, deserializedEntity1.Value2);

            Assert.IsNotNull(deserializedEntity2);
            Assert.AreEqual(entity2.Id, deserializedEntity2.Id);
            Assert.AreEqual(entity2.Value1, deserializedEntity2.Value1);
            Assert.AreEqual(entity2.Value2, deserializedEntity2.Value2);
        }

        [TestMethod]
        public void ShouldNotPersistDuplicates()
        {
            var entity1 = new TestEntity { Value1 = "Foo", Value2 = "Bar" };
            var entity2 = new TestEntity { Id = entity1.Id };   //Same Id

            Assert.AreEqual(1, _repo.Create(entity1));
            Assert.AreEqual(0, _repo.Create(entity2));
        }

        #endregion

        #region Update
        [TestMethod]
        public void ShouldPermitUpdates()
        {
            var entity = new TestEntity { Value1 = "Foo", Value2 = "Bar" };
            Assert.AreEqual(1, _repo.Create(entity));

            var persistedEntity = _repo.FindById(entity.Id);
            Assert.IsNotNull(persistedEntity);

            persistedEntity.Value1 = "ABC";
            persistedEntity.Value2 = "XYZ";
            Assert.AreEqual(1, _repo.Update(persistedEntity));

            var updateEntity = _repo.FindById(entity.Id);
            Assert.IsNotNull(updateEntity);

            Assert.IsNotNull(updateEntity);
            Assert.AreEqual(persistedEntity.Id, updateEntity.Id);
            Assert.AreEqual(persistedEntity.Value1, updateEntity.Value1);
            Assert.AreEqual(persistedEntity.Value2, updateEntity.Value2);
        }

        #endregion

        #region Delete
        [TestMethod]
        public void ShouldPermitDeletes()
        {
            var entity = new TestEntity { Value1 = "Foo", Value2 = "Bar" };
            Assert.AreEqual(1, _repo.Create(entity));

            var persistedEntity = _repo.FindById(entity.Id);
            Assert.IsNotNull(persistedEntity);

            Assert.AreEqual(1, _repo.Delete(persistedEntity));

            Assert.IsNull(_repo.FindById(entity.Id));
        }

        #endregion

        #region Find
        [TestMethod]
        public void ShouldReturnNullWhenNotFound()
        {
            var entity = _repo.FindById(Guid.NewGuid());

            Assert.IsNull(entity);
        }

        [TestMethod]
        public void ShouldFindAll()
        {
            var entity1 = new TestEntity { Value1 = "Foo", Value2 = "Bar" };
            var entity2 = new TestEntity { Value1 = "Vice", Value2 = "Roy" };

            Assert.AreEqual(1, _repo.Create(entity1));
            Assert.AreEqual(1, _repo.Create(entity2));

            var persistedEntities = _repo.FindAll().ToList();
            Assert.AreEqual(2, persistedEntities.Count());

            var persistedEntity1 = persistedEntities
                .FirstOrDefault(x => x.Id == entity1.Id);

            var persistedEntity2 = persistedEntities
                .FirstOrDefault(x => x.Id == entity2.Id);

            Assert.IsNotNull(persistedEntity1);
            Assert.AreEqual(entity1.Id, persistedEntity1.Id);
            Assert.AreEqual(entity1.Value1, persistedEntity1.Value1);
            Assert.AreEqual(entity1.Value2, persistedEntity1.Value2);

            Assert.IsNotNull(persistedEntity2);
            Assert.AreEqual(entity2.Id, persistedEntity2.Id);
            Assert.AreEqual(entity2.Value1, persistedEntity2.Value1);
            Assert.AreEqual(entity2.Value2, persistedEntity2.Value2);
        }

        #endregion

    }
}