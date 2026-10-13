using Microsoft.Extensions.Configuration;

namespace Domain.Settings
{
    public class CryptoSettings
    {
        public static string Section => nameof(CryptoSettings);
        public int EmailConfirmationCodeLenght { get; set; }
        public int PasswordHashLenght { get; set; }

        public string Pepper { get; set; } = default!;
    }

    public static class CryptoSettingsExtension
    {
        private static CryptoSettings? _cache;
        public static CryptoSettings GetCryptoSettings(this IConfiguration configuration)
        {
            if (_cache == null)
            {

                CryptoSettings? result = configuration.GetSection(CryptoSettings.Section).Get<CryptoSettings>();

                if (result == null)
                    throw new InvalidOperationException($"Секция конфигурации '{CryptoSettings.Section}' не найдена.");

                _cache = result;
            }

            return _cache;
        }
    }
}
