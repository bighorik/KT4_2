namespace WebApi.Dto.Category
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CategoryDto? ParentCategory { get; set; }
    }
}
