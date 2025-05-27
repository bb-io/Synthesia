using Apps.Synthesia.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Synthesia.Webhooks.Base
{
    public abstract class SynthesiaWebhookHandler : Invocable, IWebhookEventHandler
    {
        protected abstract List<string> SubscriptionEvents { get; }

        private readonly InvocationContext _invocationContext;
        public SynthesiaWebhookHandler(InvocationContext invocationContext) : base(invocationContext)
        {
            _invocationContext = invocationContext;
        }

        public async Task SubscribeAsync(
              IEnumerable<AuthenticationCredentialsProvider> creds,
              Dictionary<string, string> values)
        {
            var requestBody = new
            {
                events = SubscriptionEvents,
                url = values["payloadUrl"]
            };

            var request = new RestRequest("/webhooks", Method.Post)
                .AddHeader("accept", "application/json")
                .AddJsonBody(requestBody);

            await Client.ExecuteAsync(request);
        }

        public async Task UnsubscribeAsync(
    IEnumerable<AuthenticationCredentialsProvider> creds,
    Dictionary<string, string> values)
        {
            var wrapper = await GetAllWebhooks();
            var payloadUrl = values["payloadUrl"];

            var webhookToDelete = wrapper.Webhooks
                .FirstOrDefault(w => w.url == payloadUrl);

            if (webhookToDelete == null)
                return;

            var request = new RestRequest($"/webhooks/{webhookToDelete.id}", Method.Delete)
                .AddHeader("accept", "application/json");

            await Client.ExecuteAsync(request);
        }

        private async Task<WebhookListResponse> GetAllWebhooks()
        {
            var request = new RestRequest("/webhooks", Method.Get)
                .AddHeader("accept", "application/json");

            var response = await Client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<WebhookListResponse>(response.Content) ?? new WebhookListResponse();
        }
    }
}