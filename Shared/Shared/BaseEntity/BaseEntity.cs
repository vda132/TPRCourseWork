namespace Shared.BaseEntity;

public class BaseEntity<TId>
{
    public required TId Id { get; set; }
}
