using System;

namespace IntrepidProducts.Repo
{
    public class ConfigurationManager
    {
        public string? DatabaseName { get; set; }

        public void ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(DatabaseName))
            {
                throw new ArgumentException("DatabaseName not specified");
            }
        }
    }
}