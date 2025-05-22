using Newtonsoft.Json;

namespace Apps.Synthesia.Webhooks.Models
{
    public class VideoFailedPayload
    {
        [JsonProperty("data")]
        public VideoFailedData Data { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class VideoFailedData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
