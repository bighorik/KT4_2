using Domain.Enums;

namespace WebApi.Fto.UserContact
{
    public class UserContactFto
    {
        public required Guid Id { get; set; }
        public required UserContactType Type { get; set; }
        public required string Value { get; set; }
    }
}
