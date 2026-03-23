namespace Domain.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
        public bool IsArchive { get; set; }
    }
}
