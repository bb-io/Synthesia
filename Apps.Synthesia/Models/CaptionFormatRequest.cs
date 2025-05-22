using Apps.Synthesia.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Synthesia.Models
{
    public class CaptionFormatRequest
    {
        [Display("Caption format")]
        [StaticDataSource(typeof(CaptionFormatDataHandler))]
        public string? CaptionFormat { get; set; }
    }
}
