using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Albellicart.BusinessLogic;
using AutoMapper;
using Cryptowiser.Helpers;
using Cryptowiser.Models;
using Cryptowiser.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Cryptowiser.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoController : ControllerBase
    {
        private ICryptoLogic _cryptoLogic;
        private IMapper _mapper;
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


        [HttpGet("symbols")]
        public IActionResult GetAll()
        {
            var symbols = _cryptoLogic.GetSymbols(_appSettings.CryptoBaseUrl, _appSettings.ApiKey);
            var model = _mapper.Map<IList<string>>(symbols);
            return Ok(model);
        }

        [HttpGet("rates")]
        public IActionResult GetRates(string symbol, string[] convert)
        {
            var rates = _cryptoLogic.GetRates(_appSettings.CryptoBaseUrl, _appSettings.ApiKey, symbol, convert);
            var model = _mapper.Map<IList<string>>(rates);
            return Ok(model);
        }


    }
}
