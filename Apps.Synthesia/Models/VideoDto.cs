using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Apps.Synthesia.Models
{
    public class VideoDto
    {
        [JsonProperty("captions")]
        public Captions Captions { get; set; }

        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        [JsonProperty("ctaSettings")]
        public CtaSettings CtaSettings { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("download")]
        public string Download { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lastUpdatedAt")]
        public long LastUpdatedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }
    }

    public class Thumbnail
    {
        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("gif")]
        public string Gif { get; set; }
    }
}
