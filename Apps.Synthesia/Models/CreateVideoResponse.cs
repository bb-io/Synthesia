using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Synthesia.Models
{
    public class CreateVideoResponse
    {
        [JsonProperty("callbackId")]
        [Display("Callback ID")]
        public string CallbackId { get; set; }

        [JsonProperty("createdAt")]
        [Display("Created at")]
        public long CreatedAt { get; set; }

        [JsonProperty("ctaSettings")]
        [Display("CTA settings")]
        public CtaSettings CtaSettings { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lastUpdatedAt")]
        [Display("Last updated at")]
        public long LastUpdatedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }
    }

    public class CtaSettings
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
