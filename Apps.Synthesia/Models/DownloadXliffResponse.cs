using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Synthesia.Models
{
    public class DownloadXliffResponse
    {
        [Display("Video XLIFF file")]
        public FileReference File { get; set; }
    }
}
