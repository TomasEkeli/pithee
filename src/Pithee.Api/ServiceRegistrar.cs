using Pithee.Api.Users;
using Pithee.Api.Webfinger;
using Pithee.Persistence.Users;

namespace Pithee.Api;

public static class ServiceRegistrar
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services
    )
    {
        services
            .AddSingleton<IUsersRepository, UsersRepository>()
            .AddTransient<IWebFingerHandler, WebFingerHandler>()
            .AddTransient<IUsersHandler, UsersHandler>();

        return services;
    }
}