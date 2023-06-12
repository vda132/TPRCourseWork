using Shared.DatabaseInitializer;

namespace ProductService.DAL;

public class ProductDataBaseInitializer : DataBaseInitializer
{
    public ProductDataBaseInitializer(ProductContext.ProductContext context) : base(context)
    {
    }
}
