using Cryptowiser.BusinessLogic;
using AutoMapper;
using Cryptowiser.Helpers;
using Cryptowiser.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace Cryptowiser.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoLogic _cryptoLogic;
        private readonly AppSettings _appSettings;

        public CryptoController(
            ICryptoLogic cryptoLogic,
            IOptions<AppSettings> appSettings)
        {
            _cryptoLogic = cryptoLogic;
            _appSettings = appSettings.Value;
        }


        [HttpGet("symbols/{sort}")]
        public IActionResult GetAll(string sort)
        {
            if (string.IsNullOrEmpty(sort))
                return BadRequest(new { message = "Please Input Sort Order" });

            try
            {
                var symbols = _cryptoLogic.GetSymbols(_appSettings.CryptoBaseUrl, _appSettings.ApiKey, sort);
                return Ok(symbols);
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
            if (string.IsNullOrEmpty(symbol))
                return BadRequest(new { message = "Please Input Symbol" });
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
