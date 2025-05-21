using Apps.Synthesia.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Synthesia.Models
{
    public class CreateVideoFromTemplateRequest
    {
        [Display("Test video")]
        public bool Test { get; set; }

        [Display("Template ID")]
        [DataSource(typeof(TemplateDataHandler))]
        public string TemplateId { get; set; }

        [Display("Template data keys")]
        public IEnumerable<string> TemplateDataKeys { get; set; }

        [Display("Template data values")]
        public IEnumerable<string> TemplateDataValues { get; set; }

        [Display("Visibility")]
        public string? Visibility { get; set; }

        [Display("Callback ID")]
        public string? CallbackId { get; set; }

        [Display("Title")]
        public string? Title { get; set; }

        [Display("Description")]
        public string? Description { get; set; }

        [Display("CTA Label")]
        public string? CtaLabel { get; set; }

        [Display("CTA URL")]
        public string? CtaUrl { get; set; }
    }
}
