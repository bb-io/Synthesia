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
    public class VideoWebhookList : BaseInvocable
    {
        private readonly InvocationContext _invocationContext;
        public VideoWebhookList(InvocationContext invocationContext)
            : base(invocationContext)
        {
            _invocationContext = invocationContext;
        }
        [Webhook("On video completed", typeof(VideoCompletedHandler), Description = "On any video completed")]
        public Task<WebhookResponse<VideoCompletedPayload>> OnVideoCompleted(WebhookRequest webhookRequest,
            [WebhookParameter] VideoOptionFilter input)
        {
            var logger = _invocationContext.Logger;
            try
            {
                logger?.LogInformation(
                    "[Synthesia webhook] Received OnVideoCompleted event, raw body: {Body}",
                    new object[] { webhookRequest.Body.ToString() });

                var payload = webhookRequest.Body.ToString();
                ArgumentException.ThrowIfNullOrEmpty(payload, nameof(webhookRequest.Body));
                var result = JsonConvert.DeserializeObject<VideoCompletedPayload>(payload)
                             ?? throw new InvalidOperationException("Failed to deserialize VideoCompletedPayload");

                if (!string.IsNullOrWhiteSpace(input?.VideoId) &&
                    !string.Equals(result.Data.Id, input.VideoId, StringComparison.OrdinalIgnoreCase))
                {
                    logger?.LogInformation(
                        "[Synthesia webhook] Preflighting OnVideoCompleted for VideoId {FilteredVideoId}",
                        new object[] { input.VideoId });
                    return Task.FromResult(PreflightResponse<VideoCompletedPayload>());
                }

                logger?.LogInformation(
                    "[Synthesia webhook] Processing OnVideoCompleted for VideoId {VideoId}",
                    new object[] { result.Data.Id });

                return Task.FromResult(new WebhookResponse<VideoCompletedPayload>
                {
                    HttpResponseMessage = null,
                    Result = result,
                    ReceivedWebhookRequestType = WebhookRequestType.Default
                });
            }
            catch (Exception ex)
            {
                logger?.LogError(
                    "[Synthesia webhook] Exception in OnVideoCompleted: {Exception}",
                    new object[] { ex.ToString() });
                throw;
            }


            //var payload = webhookRequest.Body.ToString();
            //ArgumentException.ThrowIfNullOrEmpty(payload, nameof(webhookRequest.Body));
            //var result = JsonConvert.DeserializeObject<VideoCompletedPayload>(payload);

            //if (!string.IsNullOrWhiteSpace(input?.VideoId)&& !string.Equals(result.Data.Id, input.VideoId, StringComparison.OrdinalIgnoreCase))
            //{
            //    return Task.FromResult(PreflightResponse<VideoCompletedPayload>());
            //}
            //// add identity string [Synthesya webhook]
            //return Task.FromResult(new WebhookResponse<VideoCompletedPayload>
            //{
            //    HttpResponseMessage = null,
            //    Result = result,
            //    ReceivedWebhookRequestType = WebhookRequestType.Default
            //});
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

        private static WebhookResponse<T> PreflightResponse<T>()
        where T : class
        {
            return new WebhookResponse<T>
            {
                ReceivedWebhookRequestType = WebhookRequestType.Preflight,
                Result = null
            };
        }
    }
}
