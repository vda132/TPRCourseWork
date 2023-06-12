namespace Shared.Responce;

public class ResponceModel<TData>
{
    public TData? Data { get; set; }
    public string? ExceptionError { get; set; }
    public bool HasErrors { get; set; }
}

public class ResponceModel 
{
    public string? ExceptionError { get; set; }
    public bool HasErrors { get; set; } 
}
