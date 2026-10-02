namespace BookCatalogApi.DTOs;

public class BookCreateUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}