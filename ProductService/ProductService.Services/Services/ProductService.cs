using Microsoft.Extensions.Caching.Memory;
using ProductService.DAL.Repositories;
using ProductService.Dtos;
using Shared.Responce;

namespace ProductService.Services.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IMemoryCache memoryCache;
    private string cacheKey = "Products";

    public ProductService(IProductRepository productRepository, IMemoryCache memoryCache)
    {
        this.productRepository = productRepository;
        this.memoryCache = memoryCache;
    }

    public async Task<ResponceModel<Guid>> AddAsync(ProductDto model, CancellationToken cancellationToken)
    {
        var result = await this.productRepository.AddAsync(model, cancellationToken);
        ResponceModel<IEnumerable<ProductDto>> products;

        if (!result.HasErrors && this.memoryCache.TryGetValue(this.cacheKey, out products!))
        {
            var list = products!.Data!.ToList();

            model.Id = result.Data;
            list.Add(model);
            products.Data = list;

            this.memoryCache.Set(this.cacheKey, products,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(120)));
        }

        return result;
    }

    public async Task<ResponceModel> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await this.productRepository.DeleteAsync(id, cancellationToken);
        ResponceModel<IEnumerable<ProductDto>> products;

        if (!result.HasErrors && this.memoryCache.TryGetValue(this.cacheKey, out products!))
        {
            var list = products!.Data!.ToList();
            var productToDelete = list.Find(x => x.Id == id);
            
            list.Remove(productToDelete!);
            products.Data = list;

            this.memoryCache.Set(this.cacheKey, products,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(120)));
        }

        return result;
    }

    public async Task<ResponceModel<ProductDto>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this.productRepository.GetAsync(id, cancellationToken);
    }

    public async Task<ResponceModel<IEnumerable<ProductDto>>> GetAsync(CancellationToken cancellationToken)
    {
        ResponceModel<IEnumerable<ProductDto>> result;
        
        if (this.memoryCache.TryGetValue(this.cacheKey, out result!)) 
            return result;

        result = await this.productRepository.GetAsync(cancellationToken);

        if (result!.Data?.Count() > 0) 
            this.memoryCache.Set(this.cacheKey, result, 
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(120)));
        
        return result;
    }

    public async Task<ResponceModel<IEnumerable<ProductDto>>> GetProductsByUserId(Guid id, CancellationToken cancellationToken)
    {
        var result = await this.productRepository.GetProductsByUserId(id, cancellationToken);

        return result;
    }

    public async Task<ResponceModel<ProductDto>> UpdateAsync(ProductDto model, CancellationToken cancellationToken)
    {
        var result = await this.productRepository.UpdateAsync(model, cancellationToken);
        ResponceModel<IEnumerable<ProductDto>> products;

        if (!result.HasErrors && this.memoryCache.TryGetValue(this.cacheKey, out products!))
        {
            var list = products!.Data!.ToList();
            var productToUpdate = list.First(el => el.Id == result.Data!.Id);
            list.Remove(productToUpdate);
            list.Add(result.Data!);
            products.Data = list;

            this.memoryCache.Set(this.cacheKey, products,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(120)));
        }

        return result;
    }
}
