using Shared.BaseEntity;

namespace ProductService.DAL.Models;

public class Order : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<OrderProduct>? OrderProducts { get; set; }    
}