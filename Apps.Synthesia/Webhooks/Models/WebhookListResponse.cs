namespace Apps.Synthesia.Webhooks.Models
{
    public class WebhookListResponse
    {
        public IEnumerable<WebhookDto> Webhooks { get; set; }
    }

    public class WebhookDto
    {
        public string? id { get; set; }
        public string? url { get; set; }
        public string? status { get; set; }
        public string? secret { get; set; }
        public long? createdAt { get; set; }
        public long? lastUpdatedAt { get; set; }
    }
}
