using Microsoft.Extensions.Diagnostics.HealthChecks;
using ThrottledHealthCheck;

namespace DucksAgency.Spygame.Clientportal.Healthchecks;

public static class HealthcheckExtensions
{
    /// <summary>
    /// Зарегистрировать хелсчеки приложения
    /// </summary>
    /// <param name="services">DI-контейнер в котором регистрируются хелсчеки</param>
    /// <param name="configuration">конфигурация из которой будут взяты урл для зависимостей</param>
    /// <returns></returns>
    public static IServiceCollection RegisterHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddCheck(name: "self", check: () => HealthCheckResult.Healthy(), tags: new[] { "ready" });
        services.ThrottleHealthChecks();
        return services;
    }

    public static void MapHealthChecksEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // readiness check - проверка что приложение поднялось и готово обслуживать запросы
        endpoints.MapHealthChecks("/ready", new()
        {
            Predicate = healthCheck => healthCheck.Tags.Contains("ready")
        });
        
        endpoints.MapHealthChecks("/health");
    }
}
