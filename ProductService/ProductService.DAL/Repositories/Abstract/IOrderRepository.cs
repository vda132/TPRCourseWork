using ProductService.Dtos;
using Shared.Responce;

namespace ProductService.DAL.Repositories;

public interface IOrderRepository
{
    Task<ResponceModel<IEnumerable<OrderDto>>> GetUserOrdersAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponceModel<Guid>> CreateOrderAsync(OrderDto model, CancellationToken cancellationToken);
}
