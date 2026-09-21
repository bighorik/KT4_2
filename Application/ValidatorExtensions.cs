using FluentValidation;

namespace Application
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> Url<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(url =>
                Uri.TryCreate(url, UriKind.Absolute, out var uri)
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            .WithMessage("Поле '{PropertyName}' должно быть корректным URL-адресом.");
        }
    }
}
