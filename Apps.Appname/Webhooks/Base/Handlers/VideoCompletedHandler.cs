using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Synthesia.Webhooks.Base.Handlers
{
    public class VideoCompletedHandler : SynthesiaWebhookHandler
    {
        public VideoCompletedHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        protected override List<string> SubscriptionEvents => new List<string> { "video.completed" };
    }
}
