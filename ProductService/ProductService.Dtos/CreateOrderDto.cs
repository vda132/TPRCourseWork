using System.Diagnostics.CodeAnalysis;

namespace ProductService.Dtos;

public class CreateOrderDto
{
    [AllowNull]
    public Guid? UserId { get; set; }
    public required IEnumerable<Guid> ProductIds { get; set; }
}

public static class CreateOrderExtencion
{
    public static OrderDto ToOrderDto(this CreateOrderDto dto) =>
        new OrderDto
        {
            Id = Guid.Empty,
            Price = 0,
            ProductIds = dto.ProductIds,
            UserId = dto.UserId!.Value,
            IsActive = true
        };
}