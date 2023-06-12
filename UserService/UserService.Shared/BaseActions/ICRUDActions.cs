using UserService.Shared.BaseEntity;
using UserService.Shared.Responce;

namespace UserService.Shared.BaseActions;

public interface ICRUDActions<TId, TModel> where TModel: BaseEntity<TId>
{
    Task<ResponceModel<TModel>> GetAsync(TId id, CancellationToken cancellationToken);
    Task<ResponceModel<IEnumerable<TModel>>> GetAsync(CancellationToken cancellationToken);
    Task<ResponceModel<TId>> AddAsync(TModel model, CancellationToken cancellationToken);
    Task<ResponceModel<TModel>> UpdateAsync(TModel model, CancellationToken cancellationToken);
    Task<ResponceModel> DeleteAsync(TId id, CancellationToken cancellationToken);
}
