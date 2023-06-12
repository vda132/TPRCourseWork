using Microsoft.EntityFrameworkCore;
using ProductService.DAL.Models;
using ProductService.Dtos;
using Shared.Responce;

namespace ProductService.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext.ProductContext productContext;

    public ProductRepository(ProductContext.ProductContext productContext)
    {
        this.productContext = productContext;
    }

    public async Task<ResponceModel<Guid>> AddAsync(ProductDto model, CancellationToken cancellationToken)
    {
        try
        {
            var id = Guid.NewGuid();
            model.Id = id;
            model.CreatedDate = DateTime.UtcNow;

            await this.productContext.Products.AddAsync(model.ToEntity(), cancellationToken);
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

    public async Task<ResponceModel> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await this.productContext.Products.FindAsync(new object[] { id }, cancellationToken);

        if (product is null)
            return new ResponceModel
            {
                ExceptionError = "The product does not exist",
                HasErrors = true
            };

        this.productContext.Products.Remove(product); 
        await this.productContext.SaveChangesAsync(cancellationToken);

        return new ResponceModel
        {
            ExceptionError = null,
            HasErrors = false
        };
    }

    public async Task<ResponceModel<ProductDto>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await this.productContext.Products.FindAsync(new object[] { id }, cancellationToken);

        return new ResponceModel<ProductDto>
        {
            Data = product?.ToDto(),
            ExceptionError = product is null ? "Product not found" : null,
            HasErrors = product is null
        };
    }

    public async Task<ResponceModel<IEnumerable<ProductDto>>> GetAsync(CancellationToken cancellationToken)
    {
        var products = await this.productContext.Products.ToListAsync(cancellationToken);

        return new ResponceModel<IEnumerable<ProductDto>>
        {
            Data = products.Select(el => el.ToDto()),
            ExceptionError = null,
            HasErrors = false
        };
    }

    public async Task<ResponceModel<IEnumerable<ProductDto>>> GetProductsByUserId(Guid id, CancellationToken cancellationToken)
    {
        var products = await this.productContext.Products.Where(el => el.SellerId == id).ToListAsync(cancellationToken);

        return new ResponceModel<IEnumerable<ProductDto>>
        {
            Data = products.Select(el => el.ToDto()),
            ExceptionError = null,
            HasErrors = false
        };
    }

    public async Task<ResponceModel<ProductDto>> UpdateAsync(ProductDto model, CancellationToken cancellationToken)
    {
        var product = await this.productContext.Products.FindAsync(new object[] { model.Id }, cancellationToken);

        if (product is null)
            return new ResponceModel<ProductDto>
            {
                Data = null,
                ExceptionError = "The product does not exist",
                HasErrors = true
            };

        product.SellerId = model.SellerId;
        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;

        await this.productContext.SaveChangesAsync(cancellationToken);

        return new ResponceModel<ProductDto>
        {
            Data = product.ToDto(),
            ExceptionError = null,
            HasErrors = false
        };
    }
}
