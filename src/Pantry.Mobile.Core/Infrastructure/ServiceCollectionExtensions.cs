using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Auth0;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Refit;

namespace Pantry.Mobile.Core.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMockedPantryClientApiServiceDecorator(this IServiceCollection services, string baseUrl)
    {
        services.AddSingleton<IPantryClientApiService>(sp =>
        {
            var settings = new RefitSettings
            {
                HttpMessageHandlerFactory = () => sp.GetRequiredService<RefreshTokenDelegatingHandler>(),
            };
            var real = RestService.For<IPantryClientApiService>(baseUrl, settings);
            var mock = new DummyPantryClientApiService();
            return new MockedPantryClientApiServiceDecorator(sp.GetRequiredService<ISettingsService>(), real, mock);
        });

        return services;
    }
    
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
            var client = sp.GetRequiredService<IAuth0Client>();
            var settingsService = sp.GetRequiredService<ISettingsService>();
            var handler = new RefreshTokenDelegatingHandler(client, settingsService);
            return handler;
        });

        return services;
    }
}
