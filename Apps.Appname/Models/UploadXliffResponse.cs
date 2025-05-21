using Newtonsoft.Json;

namespace Apps.Synthesia.Models
{
    public class UploadXliffResponse
    {
        [JsonProperty("translated_video_id")]
        public string TranslatedVideoId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
