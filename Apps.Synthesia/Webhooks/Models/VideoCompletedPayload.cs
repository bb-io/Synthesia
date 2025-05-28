using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Synthesia.Webhooks.Models
{
    public class VideoCompletedPayload
    {
        [JsonProperty("data")]
        public VideoData Data { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }
    }

    public class VideoData
    {
        [JsonProperty("callbackId")]
        [Display("Callback ID")]
        public string? CallbackId { get; set; }

        [JsonProperty("captions")]
        public Captions? Captions { get; set; }

        [JsonProperty("createdAt")]
        [Display("Created at")]
        public long CreatedAt { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("download")]
        public string Download { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lastUpdatedAt")]
        [Display("Last updated at")]
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

    public class Captions
    {
        [JsonProperty("srt")]
        public string Srt { get; set; }

        [JsonProperty("vtt")]
        public string Vtt { get; set; }
    }

    public class Thumbnail
    {
        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("gif")]
        public string Gif { get; set; }
    }
}
