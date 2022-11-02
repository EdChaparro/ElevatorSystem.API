using System.IO;
using System.Text.Json;

namespace IntrepidProducts.Repo
{
    public abstract class AbstractFileRepo<TEntity> : AbstractRepo<TEntity>
        where TEntity : class
    {
        protected AbstractFileRepo(RepoConfigurationManager configManager)
        {
            ConfigManager = configManager;
        }

        protected RepoConfigurationManager ConfigManager { get; }

        private string? _entityFileName;
        protected string EntityFileName
        {
            get
            {
                return _entityFileName = _entityFileName ?? typeof(TEntity).Name;
            }
        }

        private string? _filePath;
        public string FilePath
        {
            get
            {
                return _filePath = _filePath ??
                                   Path.Combine(ConfigManager.FilePath,
                                       EntityFileName);
            }
        }

        private readonly JsonSerializerOptions JsonSerializerOptions
            = new JsonSerializerOptions { WriteIndented = true };

        private string ToJson(TEntity entity)
        {
            return JsonSerializer.Serialize(entity, JsonSerializerOptions);
        }

        private TEntity ToEntity(string json)
        {
            return JsonSerializer.Deserialize<TEntity>(json);
        }

        protected void Persist(TEntity entity)
        {
            File.WriteAllText(FilePath, ToJson(entity));
        }

        protected TEntity Deserialize(string filePath)
        {
            return ToEntity(File.ReadAllText(filePath));
        }
    }
}