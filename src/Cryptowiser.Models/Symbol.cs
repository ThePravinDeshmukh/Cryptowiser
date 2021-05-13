using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cryptowiser.Models
{
    public class Symbol : ISymbol
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonProperty("symbol")]
        public string Name { get; set; }
    }

    public interface ISymbol
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
