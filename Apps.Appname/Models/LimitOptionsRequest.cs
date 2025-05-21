using Blackbird.Applications.Sdk.Common;

namespace Apps.Appname.Models
{
    public class LimitOptionsRequest
    {
        [Display("Limit")]
        public int? Limit { get; set; } = 20;

        [Display("Offset")]
        public int? Offset { get; set; } = 0;
    }
}
