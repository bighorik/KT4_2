using Microsoft.Extensions.Configuration;

namespace Domain.Settings
{
    public class JwtSettings
    {
        public static string Section => nameof(JwtSettings);
        public string Secret { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int AccessTokenMinutes { get; set; }
        public int RefreshTokenDays { get; set; }
    }

    public static class JwtSettingsExtension
    {
        private static JwtSettings? _cache;
        public static JwtSettings GetJwtSettings(this IConfiguration configuration)
        {
            if (_cache == null)
            {

                JwtSettings? result = configuration.GetSection(JwtSettings.Section).Get<JwtSettings>();

                if (result == null)
                    throw new InvalidOperationException($"Секция конфигурации '{JwtSettings.Section}' не найдена.");

                _cache = result;
            }

            return _cache;
        }
    }
}
