using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cryptowiser.BusinessLogic;
using AutoMapper;
using Cryptowiser.Helpers;
using Cryptowiser.Models;
using Cryptowiser.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Cryptowiser.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoLogic _cryptoLogic;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CryptoController(
            ICryptoLogic cryptoLogic,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _cryptoLogic = cryptoLogic;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        [HttpGet("symbols/{sort}")]
        public IActionResult GetAll(string sort)
        {
            try
            {
                var symbols = _cryptoLogic.GetSymbols(_appSettings.CryptoBaseUrl, _appSettings.ApiKey, sort);
                var model = _mapper.Map<IList<string>>(symbols);
                return Ok(model);
            }
            catch (BadResponseException ex)
            {
                Log.Error(ex.ToString());
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("rates/{symbol}")]
        public IActionResult GetRates(string symbol, [FromBody] string[] convertTo)
        {
            try
            {
                var rates = _cryptoLogic.GetQuotes(_appSettings.CryptoBaseUrl, _appSettings.ApiKey, symbol, convertTo);
                return Ok(rates);
            }
            catch (BadResponseException ex)
            {
                Log.Error(ex.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("rates/{symbol}")]
        public IActionResult GetRates(string symbol)
        {
            try
            {
                var rates = _cryptoLogic.GetQuotes(_appSettings.CryptoBaseUrl, _appSettings.ApiKey, symbol);
                return Ok(rates);
            }
            catch (BadResponseException ex)
            {
                Log.Error(ex.ToString());
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
