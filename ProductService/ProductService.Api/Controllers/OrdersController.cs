using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dtos;
using ProductService.Services.Services;
using Shared.Constants;

namespace ProductService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrdersController : ControllerBase
{
    private readonly IOrderService orderService;

    public OrdersController(IOrderService orderService)
    {
        this.orderService = orderService;
    }

    [HttpGet("user/{id}")]
    [Authorize(Roles = $"{Roles.Admin}")]
    public async Task<IActionResult> GetOrders([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await this.orderService.GetUserOrdersAsync(id, cancellationToken);
    
        return Ok(result);
    }

    [HttpGet("by-user")]
    [Authorize(Roles = $"{Roles.Client}")]
    public async Task<IActionResult> GetUserOrders(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(el => el.Type == "id")!.Value);

        var result = await this.orderService.GetUserOrdersAsync(userId, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = $"{Roles.Client}")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(el => el.Type == "id")!.Value);
        dto.UserId = userId;

        var result = await this.orderService.CreateOrderAsync(dto.ToOrderDto(), cancellationToken);

        return Ok(result);
    }
}
