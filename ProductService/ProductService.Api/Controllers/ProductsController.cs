using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dtos;
using ProductService.Services.Services;

using Roles = Shared.Constants.Roles;

namespace ProductService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Seller}")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
        {
            if (dto.SellerId is null)
                dto.SellerId = Guid.Parse(User.FindFirst(el => el.Type == "id")!.Value);

            var result = await this.productService.AddAsync(dto.ToProductDto(), cancellationToken);

            return result.HasErrors ? BadRequest(result) : Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var result = await this.productService.GetAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await this.productService.GetAsync(id, cancellationToken);

            return result.HasErrors ? BadRequest(result) : Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Seller}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto dto, CancellationToken cancellationToken)
        {
            var result = await this.productService.UpdateAsync(dto, cancellationToken);

            return result.HasErrors ? BadRequest(result) : Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Seller}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await this.productService.DeleteAsync(id, cancellationToken);

            return result.HasErrors ? BadRequest(result) : Ok(result);
        }

        [HttpGet("get-by-user-id/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByUserId([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await this.productService.GetProductsByUserId(id, cancellationToken);

            return result.HasErrors ? BadRequest(result) : Ok(result);
        }

        [HttpGet("get-by-user")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Seller}")]
        public async Task<IActionResult> GetProductsByUser(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(User.FindFirst(el => el.Type == "id")?.Value!);

            var result = await this.productService.GetProductsByUserId(userId, cancellationToken);

            return result.HasErrors ? BadRequest(result) : Ok(result);
        }
    }
}
