﻿using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Synthesia.Models
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
        [Display("Created at")]
        public long CreatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("download")]
        public string Download { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("id")]
        [Display("Video ID")]
        public string Id { get; set; }

        [JsonProperty("lastUpdatedAt")]
        [Display("Last updated at")]
        public long LastUpdatedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("visibility")]
        public string? Visibility { get; set; }

        [JsonProperty("ctaSettings")]
        [Display("CTA settings")]
        public CtaSettings? CtaSettings { get; set; }
    }

    public class Captions
    {
        [JsonProperty("srt")]
        [Display("SRT")]
        public string? Srt { get; set; }

        [JsonProperty("vtt")]
        [Display("VTT")]
        public string? Vtt { get; set; }
    }
}
