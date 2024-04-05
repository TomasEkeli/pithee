using Pithee.Api.Signup;
using Pithee.Api.Webfinger;

namespace Pithee.Api;

public static class ServiceRegistrar
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services
    )
    {
        services
            .AddTransient<IWebFingerHandler, WebFingerHandler>()
            .AddTransient<ISignupHandler, SignupHandler>();

        return services;
    }
}