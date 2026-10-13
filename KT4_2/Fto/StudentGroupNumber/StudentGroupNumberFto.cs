namespace WebApi.Fto.StudentGroupNumber
{
    public class StudentGroupNumberFto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required bool IsArchived { get; set; }
    }
}
