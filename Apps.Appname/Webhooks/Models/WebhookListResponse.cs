using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Synthesia.Webhooks.Models
{
    public class WebhookListResponse
    {
        public string id { get; set; }
        public string url { get; set; }
        public string status { get; set; }
        public string secret { get; set; }
        public long createdAt { get; set; }
        public long lastUpdatedAt { get; set; }
    }
}
