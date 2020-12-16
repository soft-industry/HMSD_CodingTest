using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiGateway.Controllers
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
        [Route("encrypt")]
        public string Encrypt(string secret)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("decrypt")]
        public string Decrypt(string data)
        {
            throw new NotImplementedException();
        }
    }
}
