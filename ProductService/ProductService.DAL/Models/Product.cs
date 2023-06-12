using ProductService.Dtos;
using Shared.BaseEntity;

namespace ProductService.DAL.Models;

public class Product : BaseEntity<Guid>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required decimal Price { get; set; }
    public required Guid SellerId { get; set; }
    public IEnumerable<OrderProduct>? OrderProducts { get; set; }
}

public static class ProductExtencion
{
    public static Product ToEntity(this ProductDto dto) =>
        new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            CreatedDate = dto.CreatedDate,
            Price = dto.Price,
            SellerId = dto.SellerId
        };

    public static ProductDto ToDto(this Product product) =>
        new ProductDto
        {
            Id = product.Id,
            CreatedDate = product.CreatedDate,
            Price = product.Price,
            Description = product.Description,
            Name = product.Name,
            SellerId = product.SellerId
        };
}