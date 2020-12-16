using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApps.ApiGateway.Extensions;

namespace WebApps.ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ILogger<CryptoController> _logger;
        private readonly string _baseUrl;

        public CryptoController(IConfiguration configuration, ILogger<CryptoController> logger)
        {
            _logger = logger;
            _baseUrl = configuration.GetValue<string>("InternalApiBaseUrl");
        }

        [HttpGet]
        [Route("encrypt/{secret}")]
        public async Task<IActionResult> Encrypt(string secret)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(CreateInternalUri("crypto/encrypt/", secret)))
                {
                    return await response.GetActionResult();
                }
            }
        }

        [HttpGet]
        [Route("decrypt/{data}")]
        public async Task<IActionResult> Decrypt(string data)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(CreateInternalUri("crypto/decrypt/", data)))
                {
                    return await response.GetActionResult();
                }
            }
        }

        private Uri CreateInternalUri(string relativePath, string parameter = "")
        {
            var uriString = $"{_baseUrl}{relativePath}{parameter}";

            return new Uri(uriString);
        }
    }
}
