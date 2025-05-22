using Blackbird.Applications.Sdk.Common;

namespace Apps.Synthesia.Models
{
    public class LimitOptionsRequest
    {
        [Display("Limit")]
        public int? Limit { get; set; }

        [Display("Offset")]
        public int? Offset { get; set; } 
    }
}
