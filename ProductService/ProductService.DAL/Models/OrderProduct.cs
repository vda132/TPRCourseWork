namespace ProductService.DAL.Models;

public class OrderProduct
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
}
