namespace Application.DTOs;

public class ProductCatalogDto
{
    public string Name { get; set; }
    public decimal Price { get; set; } // Use decimal for currency
    public int StockQuantity { get; set; }
    public string ImageUrl { get; set; }
}