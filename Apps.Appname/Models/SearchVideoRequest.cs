using Apps.Appname.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Appname.Models
{
    public class SearchVideoRequest
    {
        [Display("Video ID")]
        [DataSource(typeof(VideoDataHandler))]
        public string? VideoId { get; set; }
    }
}
