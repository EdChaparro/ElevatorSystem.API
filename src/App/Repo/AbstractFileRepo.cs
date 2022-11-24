using IntrepidProducts.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace IntrepidProducts.Repo
{
    //Implementation intended strictly for development

    public abstract class AbstractFileRepo<TEntity> : AbstractRepo<TEntity>
        where TEntity : class, IHasId
    {
        protected AbstractFileRepo(RepoConfigurationManager configManager)
        {
            ConfigManager = configManager;

            CreateDirectory(configManager.FilePath);
        }

        private static void CreateDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                return;
            }

            Directory.CreateDirectory(dir);
        }

        private List<TEntity>? _entities = null;

        protected List<TEntity> Entities
        {
            get { return _entities ??= _entities ?? Deserialize(); }
        }

        protected override bool DoesEntityExits(TEntity entity)
        {
            return Entities.Any(x => x.Id == entity.Id);
        }

        public override int DoCreate(TEntity entity)
        {
            if (DoesEntityExits(entity))
            {
                return 0;
            }

            Entities.Add(entity);
            Persist();
            return 1;
        }

        public override int Update(TEntity entity)
        {
            var persistedEntity = FindById(entity.Id);

            if (persistedEntity == null)
            {
                return 0;
            }

            var index = Entities.IndexOf(persistedEntity);

            Entities[index] = entity;
            Persist();

            return 1;
        }

        public override int Delete(TEntity entity)
        {
            var persistedEntity = FindById(entity.Id);

            if (persistedEntity == null)
            {
                return 0;
            }

            var isDeleted = Entities.Remove(entity);

            if (!isDeleted)
            {
                return 0;
            }

            Persist();
            return 1;
        }

        public override TEntity? FindById(Guid id)
        {
            _entities = Deserialize();

            return _entities.FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<TEntity> FindAll()
        {
            return Deserialize().ToList();
        }

        protected void ClearAllEntities()
        {
            Entities.Clear();
            Persist();
        }

        protected RepoConfigurationManager ConfigManager { get; }

        private string? _entityFileName;
        protected string EntityFileName
        {
            get
            {
                return _entityFileName = _entityFileName ?? typeof(TEntity).Name + ".json";
            }
        }

        private string? _filePath;
        public string FilePath
        {
            get
            {
                return _filePath = _filePath ??
                                   Path.Combine(ConfigManager.FilePath, EntityFileName);
            }
        }

        private readonly JsonSerializerOptions JsonSerializerOptions
            = new JsonSerializerOptions { WriteIndented = true };

        private string ToJson()
        {
            return JsonSerializer.Serialize(Entities, JsonSerializerOptions);
        }

        private List<TEntity> ToEntities(string json)
        {
            return JsonSerializer.Deserialize<List<TEntity>>(json);
        }

        protected void Persist()
        {
            File.WriteAllText(FilePath, ToJson());
        }

        private List<TEntity> Deserialize()
        {
            return File.Exists(FilePath)
                ? ToEntities(File.ReadAllText(FilePath))
                : new List<TEntity>();
        }
    }
}