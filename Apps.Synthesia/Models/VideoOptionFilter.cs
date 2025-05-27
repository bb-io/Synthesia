using Apps.Synthesia.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Synthesia.Models
{
    public class VideoOptionFilter
    {
        [Display("Video ID")]
        [DataSource(typeof(VideoDataHandler))]
        public string? VideoId { get; set; }
    }
}
