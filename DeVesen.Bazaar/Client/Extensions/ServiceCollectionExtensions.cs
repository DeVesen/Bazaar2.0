using Fluxor;

namespace DeVesen.Bazaar.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureFluxor(this IServiceCollection services)
        => services.AddFluxor(config => config
            .ScanAssemblies(typeof(Program).Assembly)
            .UseReduxDevTools());
}