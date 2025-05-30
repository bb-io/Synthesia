using Apps.Synthesia.Api;
using Apps.Synthesia.Webhooks.Base.Handlers;
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
        protected readonly SynthesiaClient Client;

        public SynthesiaWebhookHandler(InvocationContext invocationContext) : base(invocationContext)
        {
            _invocationContext = invocationContext;
            Client = new SynthesiaClient(invocationContext.AuthenticationCredentialsProviders);
        }

        public async Task SubscribeAsync(
            IEnumerable<AuthenticationCredentialsProvider> creds,
            Dictionary<string, string> values)
        {
            await WebhookLogger.LogAsync($"Subscribing to webhook with URL: {values["payloadUrl"]}", "INFO");

            try
            {
                var requestBody = new
                {
                    events = SubscriptionEvents,
                    url = values["payloadUrl"]
                };

                var request = new RestRequest("/webhooks", Method.Post)
                    .AddHeader("accept", "application/json")
                    .AddJsonBody(requestBody);

                var response = await Client.ExecuteAsync(request);
                if (!response.IsSuccessful)
                {
                    await WebhookLogger.LogAsync($"Failed to subscribe webhook: {response.StatusCode}, {response.Content}", "ERROR");
                    throw new Exception("Webhook subscription failed.");
                }

                await WebhookLogger.LogAsync("Webhook subscription successful", "INFO");
            }
            catch (Exception ex)
            {
                await WebhookLogger.LogAsync($"Error subscribing webhook: {ex.Message}", "ERROR");
                throw;
            }
        }

        public async Task UnsubscribeAsync(
            IEnumerable<AuthenticationCredentialsProvider> creds,
            Dictionary<string, string> values)
        {
            await WebhookLogger.LogAsync($"Unsubscribing webhook with URL: {values["payloadUrl"]}", "INFO");

            try
            {
                var wrapper = await GetAllWebhooks();
                var payloadUrl = values["payloadUrl"];
                var webhookToDelete = wrapper.Webhooks.FirstOrDefault(w => w.url == payloadUrl);

                if (webhookToDelete == null)
                {
                    await WebhookLogger.LogAsync($"No webhook found for URL: {payloadUrl}", "WARNING");
                    return;
                }

                var request = new RestRequest($"/webhooks/{webhookToDelete.id}", Method.Delete)
                    .AddHeader("accept", "application/json");

                var response = await Client.ExecuteAsync(request);
                if (!response.IsSuccessful)
                {
                    await WebhookLogger.LogAsync($"Failed to unsubscribe webhook: {response.StatusCode}, {response.Content}", "ERROR");
                    throw new Exception("Webhook unsubscription failed.");
                }

                await WebhookLogger.LogAsync("Webhook unsubscription successful", "INFO");
            }
            catch (Exception ex)
            {
                await WebhookLogger.LogAsync($"Error unsubscribing webhook: {ex.Message}", "ERROR");
                throw;
            }
        }

        private async Task<WebhookListResponse> GetAllWebhooks()
        {
            await WebhookLogger.LogAsync("Fetching all webhooks", "DEBUG");

            try
            {
                var request = new RestRequest("/webhooks", Method.Get)
                    .AddHeader("accept", "application/json");

                var response = await Client.ExecuteAsync(request);
                var result = JsonConvert.DeserializeObject<WebhookListResponse>(response.Content) ?? new WebhookListResponse();
                await WebhookLogger.LogAsync($"Fetched webhooks: {response.Content}", "DEBUG");
                return result;
            }
            catch (Exception ex)
            {
                await WebhookLogger.LogAsync($"Error fetching webhooks: {ex.Message}", "ERROR");
                throw;
            }
        }
    }
}