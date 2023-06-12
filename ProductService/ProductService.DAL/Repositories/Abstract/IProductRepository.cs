using ProductService.Dtos;
using Shared.BaseActions;
using Shared.Responce;

namespace ProductService.DAL.Repositories;

public interface IProductRepository : ICRUDActions<Guid, ProductDto>
{
    Task<ResponceModel<IEnumerable<ProductDto>>> GetProductsByUserId(Guid id, CancellationToken cancellationToken);
}
