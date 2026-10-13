namespace Application.Utils
{
    public class PaginationQuery
    {
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
        public string? OrderBy { get; set; }
        public bool OrderByDesc { get; set; } = false;
    }
}
