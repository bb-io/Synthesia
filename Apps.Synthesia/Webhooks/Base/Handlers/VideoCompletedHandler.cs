using Apps.Synthesia.Api;
using Apps.Synthesia.Models;
using Apps.Synthesia.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.Synthesia.Webhooks.Base.Handlers
{
    public class VideoCompletedHandler(InvocationContext invocationContext, [WebhookParameter] VideoOptionFilter input)
     : SynthesiaWebhookHandler(invocationContext), IAfterSubscriptionWebhookEventHandler<VideoCompletedPayload>
    {
        protected override List<string> SubscriptionEvents => new List<string> { "video.completed" };

        public async Task<AfterSubscriptionEventResponse<VideoCompletedPayload>> OnWebhookSubscribedAsync()
        {
            if (string.IsNullOrWhiteSpace(input.VideoId))
                return null;

            var client = new SynthesiaClient(InvocationContext.AuthenticationCredentialsProviders);
            var request = new RestRequest($"/videos/{input.VideoId}", Method.Get);
            var video = await client.ExecuteWithErrorHandling<VideoDto>(request);

            if (!string.Equals(video.Status, "complete", StringComparison.OrdinalIgnoreCase))
                return null;


            var payload = new VideoCompletedPayload
            {
                Type = SubscriptionEvents[0],
                Data = new VideoData
                {
                    CallbackId = "",
                    Captions = new Webhooks.Models.Captions
                    {
                        Srt = video.Captions.Srt ?? "",
                        Vtt = video.Captions.Vtt ?? ""
                    },
                    CreatedAt = video.CreatedAt,
                    Description = video.Description,
                    Download = video.Download,
                    Duration = video.Duration,
                    Id = video.Id,
                    LastUpdatedAt = video.LastUpdatedAt,
                    Status = video.Status,
                    Thumbnail = new Webhooks.Models.Thumbnail
                    {
                        Image = video.Thumbnail.Image,
                        Gif = video.Thumbnail.Gif
                    },
                    Title = video.Title,
                    Visibility = video.Visibility
                }
            };

            return new AfterSubscriptionEventResponse<VideoCompletedPayload>
            {
                Result = payload
            };
        }
    }
}