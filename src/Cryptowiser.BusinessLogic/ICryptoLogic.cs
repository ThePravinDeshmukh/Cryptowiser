using Cryptowiser.Models;
using System.Collections.Generic;

namespace Albellicart.BusinessLogic
{
    public interface ICryptoLogic
    {
        public IEnumerable<string> GetSymbols(string cryptobaseurl, string key);
        public IEnumerable<string> GetRates(string cryptobaseurl, string key, string symbol, string[] convert);
    }
}