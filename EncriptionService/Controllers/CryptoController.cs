using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps.EncriptionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ILogger<CryptoController> _logger;

        public CryptoController(ILogger<CryptoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("encrypt/{secret}")]
        public async Task<IActionResult> Encrypt(string secret)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("decrypt/{data}")]
        public async Task<IActionResult> Decrypt(string data)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("rotate")]
        public void RotateEncryptionKey()
        {
            throw new NotImplementedException();
        }
    }
}
