using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Refit;

namespace Pantry.Mobile.Core.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRefitClient<T>(this IServiceCollection services, string baseUrl)
        where T : class
    {
        services.AddSingleton(sp =>
        {
            var settings = new RefitSettings
            {
                AuthorizationHeaderValueGetter = async () => { var credential = await sp.GetRequiredService<ISettingsService>().GetCredentials(); return credential.AccessToken; }
            };
            return RestService.For<T>(baseUrl, settings);
        });

        return services;
    }
}
