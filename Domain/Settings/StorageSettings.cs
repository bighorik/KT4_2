using Microsoft.Extensions.Configuration;

namespace Domain.Settings
{
    public class StorageSettings
    {
        public static string Section => nameof(StorageSettings);
        public string Endpoint { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string Bucket { get; set; } = string.Empty;
        public bool UseSsl { get; set; }
    }

    public static class StorageSettingsExtension
    {
        private static StorageSettings? _cache;
        public static StorageSettings GetStorageSettings(this IConfiguration configuration)
        {
            if (_cache == null)
            {
                StorageSettings? result = configuration.GetSection(StorageSettings.Section).Get<StorageSettings>();

                if (result == null)
                    throw new InvalidOperationException($"Секция конфигурации '{StorageSettings.Section}' не найдена.");
                _cache = result;
            }

            return _cache;
        }
    }
}
