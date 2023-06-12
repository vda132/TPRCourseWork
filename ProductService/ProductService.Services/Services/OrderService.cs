using ProductService.DAL.Repositories;
using ProductService.Dtos;
using Shared.Responce;

namespace ProductService.Services.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<ResponceModel<Guid>> CreateOrderAsync(OrderDto model, CancellationToken cancellationToken)
    {
        return await this.orderRepository.CreateOrderAsync(model, cancellationToken);
    }

    public async Task<ResponceModel<IEnumerable<OrderDto>>> GetUserOrdersAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await this.orderRepository.GetUserOrdersAsync(userId, cancellationToken);
    }
}
