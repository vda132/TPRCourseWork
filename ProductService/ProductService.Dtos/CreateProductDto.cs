using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProductService.Dtos;

public class CreateProductDto
{
    [Required]
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedDate { get; set; }

    [Required]
    public required decimal Price { get; set; }

    [AllowNull]
    public Guid? SellerId { get; set; }
}

public static class ProductExtencion
{
    public static ProductDto ToProductDto(this CreateProductDto dto) =>
        new ProductDto
        {
            Id = Guid.Empty,
            Name = dto.Name,
            Description = dto.Description,
            CreatedDate = dto.CreatedDate,
            SellerId = dto.SellerId!.Value,
            Price = dto.Price
        };
}