using Apps.Synthesia.Api;
using Apps.Synthesia.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.Synthesia.Webhooks.Base.Handlers
{
    public class VideoCompletedHandler(InvocationContext invocationContext,[WebhookParameter] VideoOptionFilter input)
     : SynthesiaWebhookHandler(invocationContext), IAfterSubscriptionWebhookEventHandler<VideoDto>
    {
        protected override List<string> SubscriptionEvents => new List<string> { "video.completed" };

        public async Task<AfterSubscriptionEventResponse<VideoDto>> OnWebhookSubscribedAsync()
        {
            if (string.IsNullOrWhiteSpace(input.VideoId))
                return null;

            var client = new SynthesiaClient(InvocationContext.AuthenticationCredentialsProviders);
            var request = new RestRequest($"/webhooks/{input}", Method.Get);
            var video = await client.ExecuteWithErrorHandling<VideoDto>(request);

            if (video.Status.Equals("complete", StringComparison.OrdinalIgnoreCase))
            {
                return new AfterSubscriptionEventResponse<VideoDto>
                {
                    Result = video
                };
            }

            return null;
        }
    }
}
