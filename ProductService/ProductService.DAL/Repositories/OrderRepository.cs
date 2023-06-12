using Microsoft.EntityFrameworkCore;
using ProductService.DAL.Models;
using ProductService.Dtos;
using Shared.Responce;

namespace ProductService.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ProductContext.ProductContext productContext;

    public OrderRepository(ProductContext.ProductContext productContext)
    {
        this.productContext = productContext;
    }

    public async Task<ResponceModel<Guid>> CreateOrderAsync(OrderDto model, CancellationToken cancellationToken)
    {
        try 
        {
            var  id = Guid.NewGuid();
            model.Id = id;

            var price = await this.productContext.Products.Where(el => model.ProductIds.Contains(el.Id)).SumAsync(el => el.Price, cancellationToken);

            await this.productContext.Orders.AddAsync(new Order { Id = model.Id, UserId = model.UserId, Price = price, IsActive = true }, cancellationToken);
            await this.productContext.SaveChangesAsync(cancellationToken);

            List<OrderProduct> orderProducts = new List<OrderProduct>();
            
            foreach (var productId in model.ProductIds)
                orderProducts.Add(new OrderProduct { ProductId = productId, OrderId = id });

            await this.productContext.OrderProducts.AddRangeAsync(orderProducts, cancellationToken);
            await this.productContext.SaveChangesAsync(cancellationToken);

            return new ResponceModel<Guid>
            {
                Data = id,
                ExceptionError = null,
                HasErrors = false
            };
        }
        catch (Exception ex) 
        {
            return new ResponceModel<Guid>
            {
                Data = default,
                ExceptionError = ex.Message,
                HasErrors = true
            };
        }


    }

    public async Task<ResponceModel<IEnumerable<OrderDto>>> GetUserOrdersAsync(Guid userId, CancellationToken cancellationToken)
    {
        List<OrderDto> results = new List<OrderDto>();

        var ordersByUser = await this.productContext.Orders.Where(o => o.UserId == userId).ToListAsync();
        var orderProducts = await this.productContext.OrderProducts.Where(el => ordersByUser.Select(el => el.Id).Contains(el.OrderId)).ToListAsync();
        
        foreach (var order in ordersByUser)
        {
            var productIds = orderProducts.Where(el => el.OrderId == order.Id).Select(el => el.ProductId).ToList();

            results.Add(new OrderDto
            {
                Id = order.Id,
                ProductIds = productIds,
                UserId = userId,
                Products = await this.productContext.Products
                        .Where(el => productIds.Contains(el.Id)).Select(el => el.ToDto()).ToListAsync(),
                Price = order.Price
            });
        }

        return new ResponceModel<IEnumerable<OrderDto>>
        {
            Data = results,
            ExceptionError = null,
            HasErrors = false
        };
    }
}
