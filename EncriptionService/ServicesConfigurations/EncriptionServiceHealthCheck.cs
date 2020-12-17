using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace WebApps.EncriptionService.ServicesConfigurations
{
    public class EncriptionServiceHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _config;

        public EncriptionServiceHealthCheck(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthCheckResultHealthy = false;
            var unhealthy = "appsettings.json";

            if (_config != null)
            {
                var dirName = _config.GetValue<string>("KeyStorageFolder");

                healthCheckResultHealthy = !string.IsNullOrEmpty(dirName);
            }

            if (healthCheckResultHealthy)
            {
                return await Task.FromResult(HealthCheckResult.Healthy("EncriptionService is OK!"));
            }
            else
            {
                return await Task.FromResult(
                    HealthCheckResult.Unhealthy($"EncriptionService is Unhealthy! Please check the {unhealthy}"));
            }
        }
    }
}
