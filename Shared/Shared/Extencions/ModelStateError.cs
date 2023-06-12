using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Responce;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Extencions;

public static class ModelStateError
{
    public static IMvcCoreBuilder ConfigureModelStateErrors(this IServiceCollection services)
    {
        return services.AddMvcCore().ConfigureApiBehaviorOptions(opts =>
        {
            opts.InvalidModelStateResponseFactory =
                          (context => new BadRequestObjectResult(
                             JsonSerializer.Serialize(context.ModelState.ToImmutableDictionary(),
                             new JsonSerializerOptions { DictionaryKeyPolicy = JsonNamingPolicy.CamelCase, PropertyNamingPolicy = JsonNamingPolicy.CamelCase })));
        });
    }
}
