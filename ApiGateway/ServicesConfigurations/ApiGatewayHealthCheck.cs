using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApps.ApiGateway.ServicesConfigurations
{
    public class ApiGatewayHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _config;

        public ApiGatewayHealthCheck(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthCheckResultHealthy = false;
            string unhealthy = "appsettings.json";

            if (_config != null)
            {
                var baseUrl = _config.GetValue<string>("InternalApiBaseUrl");

                if (!string.IsNullOrEmpty(baseUrl))
                {
                    using (var client = new HttpClient())
                    {
                        using (var response = await client.GetAsync(baseUrl + "health"))
                        {
                            healthCheckResultHealthy = response.IsSuccessStatusCode;

                            if (!healthCheckResultHealthy)
                            {
                                unhealthy = "Internal API";
                            }
                        }
                    }
                }
            }

            if (healthCheckResultHealthy)
            {
                return await Task.FromResult(HealthCheckResult.Healthy("ApiGateway is OK!"));
            }
            else
            {
                return await Task.FromResult(
                    HealthCheckResult.Unhealthy($"ApiGateway is Unhealthy! Please check the {unhealthy}"));
            }
        }
    }
}
