namespace WebApi.Dto.Category
{
    public class CategoryToCreateDto
    {
        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }
    }
}
