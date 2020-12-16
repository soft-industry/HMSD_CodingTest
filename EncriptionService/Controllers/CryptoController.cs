using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApps.EncriptionService.Helpers;
using WebApps.EncriptionService.Models;

namespace WebApps.EncriptionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ILogger<CryptoController> _logger;
        private readonly string _dirName;

        public CryptoController(IConfiguration configuration, ILogger<CryptoController> logger)
        {
            _logger = logger;
            _dirName = configuration.GetValue<string>("KeyStorageFolder");
        }

        [HttpPost]
        [Route("encrypt")]
        public IActionResult Encrypt([FromBody] EncryptParameters encryptParameters)
        {
            if (encryptParameters == null)
            {
                return BadRequest();
            }

            return Ok(Cryptography.Encrypt(_dirName, encryptParameters.Secret));
        }

        [HttpPost]
        [Route("decrypt")]
        public IActionResult Decrypt([FromBody] DecryptParameters decryptParameters)
        {
            if (decryptParameters == null)
            {
                return BadRequest();
            }

            return Ok(Cryptography.Decrypt(_dirName, decryptParameters.Data));
        }

        [HttpPost]
        [Route("rotate")]
        public void RotateEncryptionKey()
        {
            Cryptography.DeleteKey(_dirName);
            Cryptography.CreateKey(_dirName);
        }
    }
}
