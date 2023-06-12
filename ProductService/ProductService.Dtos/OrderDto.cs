using Shared.BaseEntity;
using System.Diagnostics.CodeAnalysis;

namespace ProductService.Dtos;

public class OrderDto : BaseEntity<Guid>
{
    public required Guid UserId { get; set; }
    public required IEnumerable<Guid> ProductIds { get; set; }
    
    [AllowNull]
    public IEnumerable<ProductDto>? Products { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}
