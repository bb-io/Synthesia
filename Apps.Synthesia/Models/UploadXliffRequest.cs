using Apps.Synthesia.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Synthesia.Models
{
    public class UploadXliffRequest
    {
        [Display("Video ID")]
        [DataSource(typeof(VideoDataHandler))]
        public string VideoId { get; set; }

        [Display("XLIFF File")]
        public FileReference XliffFile { get; set; } = default!;

        [Display("Callback ID")]
        public string? CallbackId { get; set; }
    }
}
