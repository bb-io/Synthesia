using Apps.Synthesia.Models;
using Newtonsoft.Json;

namespace Apps.Synthesia.Webhooks.Models
{
    public class ListWebhookVideosResponse
    {
        [JsonProperty("nextOffset")]
        public int? NextOffset { get; set; }

        [JsonProperty("videos")]
        public IEnumerable<Video> Videos { get; set; }
    }
}
