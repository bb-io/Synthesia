using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Synthesia.Webhooks.Base.Handlers
{
    internal class VideoFailedHandler : SynthesiaWebhookHandler
    {
        public VideoFailedHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        protected override List<string> SubscriptionEvents => new List<string> { "video.failed" };
    }
}
