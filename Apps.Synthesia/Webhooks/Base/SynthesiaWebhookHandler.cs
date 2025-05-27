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
            var payloadUrl = values.GetValueOrDefault("payloadUrl");
            try
            {
                var requestBody = new
                {
                    events = SubscriptionEvents,
                    url = payloadUrl
                };

                var request = new RestRequest("/webhooks", Method.Post)
                    .AddHeader("accept", "application/json")
                    .AddJsonBody(requestBody);

                var response = await Client.ExecuteAsync(request);
                var content = response.Content ?? string.Empty;

                if (!response.IsSuccessful)
                {
                    _invocationContext.Logger?.LogError(
                        "[Synthesia Logger AR] Failed to subscribe to webhooks. StatusCode: {StatusCode}, Response: {ResponseContent}",
                        new object[] { response.StatusCode, content }
                    );
                }
                else
                {
                    _invocationContext.Logger?.LogInformation(
                        "[Synthesia Logger AR] Successfully subscribed to webhooks for URL {PayloadUrl} with events {Events}",
                            null);
                }
            }
            catch (Exception ex)
            {
                _invocationContext.Logger?.LogError(
                    "[Synthesia Logger AR] Exception while subscribing to webhooks for URL {PayloadUrl}: {Exception}",
                    new object[] { payloadUrl, ex }
                );
            }
        }

        public async Task UnsubscribeAsync(
            IEnumerable<AuthenticationCredentialsProvider> creds,
            Dictionary<string, string> values)
        {
            var payloadUrl = values.GetValueOrDefault("payloadUrl");
            try
            {
                var wrapper = await GetAllWebhooks();
                var webhookToDelete = wrapper.Webhooks
                    .FirstOrDefault(w => w.url == payloadUrl);

                if (webhookToDelete == null)
                {
                    _invocationContext.Logger?.LogWarning(
                        "[Synthesia Logger AR] No webhook found with URL {PayloadUrl} to unsubscribe",
                        new object[] { payloadUrl }
                    );
                    return;
                }

                var request = new RestRequest($"/webhooks/{webhookToDelete.id}", Method.Delete)
                    .AddHeader("accept", "application/json");

                var response = await Client.ExecuteAsync(request);
                var content = response.Content ?? string.Empty;

                if (!response.IsSuccessful)
                {
                    _invocationContext.Logger?.LogError(
                        "[Synthesia Logger AR] Failed to unsubscribe webhook {WebhookId}. StatusCode: {StatusCode}, Response: {ResponseContent}",
                        new object[] { webhookToDelete.id, response.StatusCode, content }
                    );
                }
                else
                {
                    _invocationContext.Logger?.LogInformation(
                            "[Synthesia Logger AR] No webhook found to unsubscribe",
                            null);
                }
            }
            catch (Exception ex)
            {
                _invocationContext.Logger?.LogError(
                    "[Synthesia Logger AR] Exception while unsubscribing webhooks for URL {PayloadUrl}: {Exception}",
                    new object[] { payloadUrl, ex }
                );
            }
        }

        private async Task<WebhookListResponse> GetAllWebhooks()
        {
            try
            {
                var request = new RestRequest("/webhooks", Method.Get)
                    .AddHeader("accept", "application/json");

                var response = await Client.ExecuteAsync(request);
                var content = response.Content ?? string.Empty;

                if (!response.IsSuccessful)
                {
                    _invocationContext.Logger?.LogError(
                        "[Synthesia Logger AR] Failed to retrieve webhook list. StatusCode: {StatusCode}, Response: {ResponseContent}",
                        new object[] { response.StatusCode, content }
                    );
                    return new WebhookListResponse();
                }

                return JsonConvert
                    .DeserializeObject<WebhookListResponse>(content)
                    ?? new WebhookListResponse();
            }
            catch (Exception ex)
            {
                _invocationContext.Logger?.LogError(
                    "[Synthesia Logger AR] Exception while retrieving webhook list: {Exception}",
                    new object[] { ex }
                );
                return new WebhookListResponse();
            }
        }





        //    public async Task SubscribeAsync(
        //          IEnumerable<AuthenticationCredentialsProvider> creds,
        //          Dictionary<string, string> values)
        //    {
        //        //_invocationContext.Logger?.LogError($"Access token not found in response. Parameters: {parameters}, Status code: {response.StatusCode}, Response: {responseContent}", []);
        //        var requestBody = new
        //        {
        //            events = SubscriptionEvents,
        //            url = values["payloadUrl"]
        //        };

        //        var request = new RestRequest("/webhooks", Method.Post)
        //            .AddHeader("accept", "application/json")
        //            .AddJsonBody(requestBody);

        //        await Client.ExecuteAsync(request);
        //    }

        //    public async Task UnsubscribeAsync(
        //IEnumerable<AuthenticationCredentialsProvider> creds,
        //Dictionary<string, string> values)
        //    {
        //        var wrapper = await GetAllWebhooks(); 
        //        var payloadUrl = values["payloadUrl"];

        //        var webhookToDelete = wrapper.Webhooks
        //            .FirstOrDefault(w => w.url == payloadUrl);

        //        if (webhookToDelete == null)
        //            return;

        //        var request = new RestRequest($"/webhooks/{webhookToDelete.id}", Method.Delete)
        //            .AddHeader("accept", "application/json");

        //        await Client.ExecuteAsync(request);
        //    }

        //    private async Task<WebhookListResponse> GetAllWebhooks()
        //    {
        //        var request = new RestRequest("/webhooks", Method.Get)
        //            .AddHeader("accept", "application/json");

        //        var response = await Client.ExecuteAsync(request);
        //        return JsonConvert.DeserializeObject<WebhookListResponse>(response.Content) ?? new WebhookListResponse();
        //    }//error unsubscribe
    }
}
