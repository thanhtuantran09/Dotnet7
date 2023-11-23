namespace WebApplication2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        // Navigation property
        public List<Product>? products { get; set; }
    }
}
