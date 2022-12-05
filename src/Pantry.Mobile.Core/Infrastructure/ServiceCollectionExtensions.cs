using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Auth0;
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
                HttpMessageHandlerFactory = () => sp.GetRequiredService<RefreshTokenDelegatingHandler>(),
                //AuthorizationHeaderValueGetter = async () => { var credential = await sp.GetRequiredService<ISettingsService>().GetCredentials(); return credential.AccessToken; }
            };
            return RestService.For<T>(baseUrl, settings);
        });

        return services;
    }

    public static IServiceCollection AddRefreshTokenDelegatingHandler(this IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            var client = sp.GetRequiredService<Auth0Client>();
            var settingsService = sp.GetRequiredService<ISettingsService>();
            var handler = new RefreshTokenDelegatingHandler(client, settingsService);
            return handler;
        });

        return services;
    }
}
