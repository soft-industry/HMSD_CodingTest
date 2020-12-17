using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApps.ApiGateway.Extensions;
using WebApps.ApiGateway.Models;

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

        [HttpPost]
        [Route("encrypt")]
        public async Task<IActionResult> Encrypt([FromBody] EncryptParameters encryptParameters)
        {
            using (var client = new HttpClient())
            {
                client.SetAcceptEncodingHeader(this.HttpContext.Request.Headers);

                var content = new StringContent(JsonConvert.SerializeObject(encryptParameters), Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync(CreateInternalUri("crypto/encrypt"), content))
                {
                    return await response.GetActionResult();
                }
            }
        }

        [HttpPost]
        [Route("decrypt")]
        public async Task<IActionResult> Decrypt([FromBody] DecryptParameters decryptParameters)
        {
            using (var client = new HttpClient())
            {
                client.SetAcceptEncodingHeader(this.HttpContext.Request.Headers);

                var content = new StringContent(JsonConvert.SerializeObject(decryptParameters), Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync(CreateInternalUri("crypto/decrypt"), content))
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
