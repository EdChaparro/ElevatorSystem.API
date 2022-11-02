using System;
using System.IO;

namespace IntrepidProducts.Repo
{
    public class RepoConfigurationManager
    {
        public RepoConfigurationManager
            (string databaseName, string? databaseDirectory = null)
        {
            DatabaseName = databaseName;

            DatabaseBaseDirectory ??= AppDomain.CurrentDomain.BaseDirectory;
        }

        public string DatabaseName { get; set; }
        public string DatabaseBaseDirectory { get; set; }

        private string? _filePath;
        public string FilePath
        {
            get
            {
                return _filePath = _filePath ??
                                   Path.Combine(DatabaseBaseDirectory, DatabaseName);
            }
        }

        public void ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(DatabaseName))
            {
                throw new ArgumentException("DatabaseName not specified");
            }

            if (string.IsNullOrEmpty(DatabaseBaseDirectory))
            {
                throw new ArgumentException("DatabaseBaseDirectory not specified");
            }
        }
    }
}