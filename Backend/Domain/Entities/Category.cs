namespace Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    // Optional: For hierarchical categories (e.g., Clothing -> Shirts)
    public int? ParentCategoryId { get; set; } 
    
    // Navigation Properties
    public ICollection<Product> Products { get; set; }
    public Category ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; }
}