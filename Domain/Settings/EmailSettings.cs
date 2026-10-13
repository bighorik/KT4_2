using Microsoft.Extensions.Configuration;

namespace Domain.Settings
{
    public class EmailSettings
    {
        public static string Section => nameof(EmailSettings);
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 587;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } = true;
    }
    public static class EmailSettingsExtension
    {
        private static EmailSettings? _cache;
        public static EmailSettings GetEmailSettings(this IConfiguration configuration)
        {
            if (_cache == null)
            {
                EmailSettings? result = configuration.GetSection(EmailSettings.Section).Get<EmailSettings>();

                if (result == null)
                    throw new InvalidOperationException($"Секция конфигурации '{EmailSettings.Section}' не найдена.");
                _cache = result;
            }

            return _cache;
        }
    }
}
