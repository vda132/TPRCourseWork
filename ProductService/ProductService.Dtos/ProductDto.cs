using Shared.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Dtos;

public class ProductDto : BaseEntity<Guid>
{
    [Required]
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedDate { get; set; }

    [Required]
    public required decimal Price { get; set; }

    public required Guid SellerId { get; set; }
}
