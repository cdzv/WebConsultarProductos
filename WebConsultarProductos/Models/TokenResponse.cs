using Newtonsoft.Json;

namespace WebConsultarProductos.Models
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public int Expiration { get; set; }

        [JsonProperty("token_type")]
        public string Type { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
