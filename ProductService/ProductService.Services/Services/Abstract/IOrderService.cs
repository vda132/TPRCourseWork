using ProductService.Dtos;
using Shared.Responce;

namespace ProductService.Services.Services;

public interface IOrderService
{
    Task<ResponceModel<IEnumerable<OrderDto>>> GetUserOrdersAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponceModel<Guid>> CreateOrderAsync(OrderDto model, CancellationToken cancellationToken);
}
