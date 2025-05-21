using Newtonsoft.Json;

namespace Apps.Appname.Models
{
    public class ListVideosResponse
    {
        [JsonProperty("videos")]
        public IEnumerable<Video> Videos { get; set; }
    }

    public class Video
    {
        [JsonProperty("captions")]
        public Captions Captions { get; set; }

        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

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

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("visibility")]
        public string? Visibility { get; set; }

        [JsonProperty("ctaSettings")]
        public CtaSettings? CtaSettings { get; set; }
    }

    public class Captions
    {
        [JsonProperty("srt")]
        public string? Srt { get; set; }

        [JsonProperty("vtt")]
        public string? Vtt { get; set; }
    }

    public class CtaSettings
    {
        [JsonProperty("label")]
        public string? Label { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }
}
