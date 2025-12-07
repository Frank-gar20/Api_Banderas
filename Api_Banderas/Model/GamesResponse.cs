using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RawGames.Model
{
    public class Country
    {
        [JsonPropertyName("name")]
        public CountryName Name { get; set; } = new();

        [JsonPropertyName("capital")]
        public List<string>? Capital { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; } = string.Empty;

        [JsonPropertyName("population")]
        public long Population { get; set; }

        [JsonPropertyName("flags")]
        public CountryFlags Flags { get; set; } = new();

        // Para mostrar solo una capital
        public string CapitalText =>
            Capital != null && Capital.Count > 0 ? Capital[0] : "Sin capital";
    }

    public class CountryName
    {
        [JsonPropertyName("Comun")]
        public string Common { get; set; } = string.Empty;
    }

    public class CountryFlags
    {
        [JsonPropertyName("png")]
        public string Png { get; set; } = string.Empty;
    }
}
