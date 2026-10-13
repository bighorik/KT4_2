using Domain.Enums;

namespace WebApi.Fto.InternetResourceLink
{
    public class InternetResourceLinkFto
    {
        public required Guid Id { get; set; }
        public required InternetResourceLinkType Type { get; set; }
        public required string Url { get; set; }
    }
}
