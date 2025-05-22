using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Synthesia.Models
{
    public class UploadXliffResponse
    {
        [JsonProperty("translated_video_id")]
        [Display("Tranlated video ID")]
        public string TranslatedVideoId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
