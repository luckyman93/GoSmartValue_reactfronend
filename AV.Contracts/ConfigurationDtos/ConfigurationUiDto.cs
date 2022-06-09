using Newtonsoft.Json;

namespace AV.Contracts.ConfigurationDtos
{
    public class ConfigurationUiDto
    {
        [JsonProperty("API_KEY")]
        public string API_KEY { get; set; }
        [JsonProperty("API_URL")]
        public string API_URL { get; set; }
        [JsonProperty("HOST")]
        public string HOST { get; set; }
        [JsonProperty("FIRE_API_KEY")]
        public string FIRE_API_KEY { get; set; }
        [JsonProperty("FIRE_AUTH_DOMAIN")]
        public string FIRE_AUTH_DOMAIN { get; set; }
        [JsonProperty("FIRE_PROJECT_ID")]
        public string FIRE_PROJECT_ID { get; set; }
        [JsonProperty("FIRE_STORAGE_BUCKETID")]
        public string FIRE_STORAGE_BUCKETID { get; set; }
        [JsonProperty("FIRE_MESSAGING_SENDER_ID")]
        public string FIRE_MESSAGING_SENDER_ID { get; set; }
        [JsonProperty("FIRE_APP_ID")]
        public string FIRE_APP_ID { get; set; }
        [JsonProperty("FIRE_MEASUREMENT_ID")]
        public string FIRE_MEASUREMENT_ID { get; set; }
    }
}