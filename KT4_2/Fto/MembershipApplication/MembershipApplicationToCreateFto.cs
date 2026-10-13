namespace WebApi.Fto.MembershipApplication
{
    public class MembershipApplicationToCreateFto
    {
        public required Guid PositionId { get; set; }
        public string Message { get; set; } = "";
    }
}
