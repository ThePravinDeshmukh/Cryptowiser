using Cryptowiser.Models;
using System.Collections.Generic;

namespace Cryptowiser.BusinessLogic
{
    public interface ICryptoLogic
    {
        public IEnumerable<string> GetSymbols(string cryptobaseurl, string key, string sort);
        public IEnumerable<SymbolRate> GetQuotes(string cryptobaseurl, string key, string symbol, string[] convertTo);
        public IEnumerable<SymbolRate> GetQuotes(string cryptobaseurl, string key, string symbol);
    }
}