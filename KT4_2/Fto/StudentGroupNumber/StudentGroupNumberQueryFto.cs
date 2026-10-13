using Application.Utils;

namespace WebApi.Fto.StudentGroupNumber
{
    public class StudentGroupNumberQueryFto : PaginationQuery
    {
        public string? Name { get; set; }
    }
}
