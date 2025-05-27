using Apps.Synthesia.Models;
using Apps.Synthesia.Webhooks.Base.Handlers;
using Apps.Synthesia.Webhooks.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Synthesia.Webhooks
{
    [WebhookList]
    public class VideoWebhookList(InvocationContext invocationContext) : BaseInvocable(invocationContext)
    {
        [Webhook("On video completed", typeof(VideoCompletedHandler), Description = "On any video completed")]
        public Task<WebhookResponse<VideoCompletedPayload>> OnVideoCompleted(WebhookRequest webhookRequest,
            [WebhookParameter] VideoOptionFilter input)
        {
            var payload = webhookRequest.Body.ToString();
            ArgumentException.ThrowIfNullOrEmpty(payload, nameof(webhookRequest.Body));
            var result = JsonConvert.DeserializeObject<VideoCompletedPayload>(payload);

            if (!string.IsNullOrWhiteSpace(input?.VideoId)
        && !string.Equals(result.Data.Id, input.VideoId, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(new WebhookResponse<VideoCompletedPayload>
                {
                    HttpResponseMessage = null,
                    Result = null!,
                    ReceivedWebhookRequestType = WebhookRequestType.Default
                });
            }

            return Task.FromResult(new WebhookResponse<VideoCompletedPayload>
            {
                HttpResponseMessage = null,
                Result = result,
                ReceivedWebhookRequestType = WebhookRequestType.Default
            });
        }

        [Webhook("On video failed", typeof(VideoFailedHandler), Description = "On any video failed")]
        public Task<WebhookResponse<VideoFailedPayload>> OnVideoFailed(WebhookRequest webhookRequest)
        {
            var payload = webhookRequest.Body.ToString();
            ArgumentException.ThrowIfNullOrEmpty(payload, nameof(webhookRequest.Body));
            var result = JsonConvert.DeserializeObject<VideoFailedPayload>(payload);

            return Task.FromResult(new WebhookResponse<VideoFailedPayload>
            {
                HttpResponseMessage = null,
                Result = result,
                ReceivedWebhookRequestType = WebhookRequestType.Default
            });
        }
    }
}
