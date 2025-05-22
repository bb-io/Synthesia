using Apps.Synthesia.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Synthesia.Models
{
    public class SearchVideosRequest
    {
        [Display("Source")]
        [StaticDataSource(typeof(SourceDataHandler))]
        public string? Source { get; set; }
    }
}
