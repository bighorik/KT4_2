namespace Domain.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public CategoryEntity? ParentCategory { get; set; }
    }
}
