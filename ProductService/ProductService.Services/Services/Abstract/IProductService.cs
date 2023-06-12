using ProductService.Dtos;
using Shared.BaseActions;
using Shared.Responce;

namespace ProductService.Services.Services;

public interface IProductService : ICRUDActions<Guid, ProductDto> 
{
    Task<ResponceModel<IEnumerable<ProductDto>>> GetProductsByUserId(Guid id, CancellationToken cancellationToken);
}
