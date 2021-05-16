using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptowiser.Models
{

    public record SymbolRate
    {
        public string Name { get; init; }
        public PriceDetail PriceDetail { get; init; }
    }
    public record PriceDetail
    {
        [JsonProperty("price")]
        public float Price { get; init; }
        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; init; }
    }
}
