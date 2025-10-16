namespace Application.DTOs;

public class UpdateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; } // Use decimal for currency
    public int StockQuantity { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
}