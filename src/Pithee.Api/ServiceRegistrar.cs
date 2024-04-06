using Pithee.Api.Users;
using Pithee.Api.Webfinger;
using Pithee.Persistence;

namespace Pithee.Api;

public static class ServiceRegistrar
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services
    )
    {
        services
            .AddTransient<IWebFingerHandler, WebFingerHandler>()
            .AddTransient<IUsersHandler, UsersHandler>()
            .RegisterPersistenceServices();

        return services;
    }
}