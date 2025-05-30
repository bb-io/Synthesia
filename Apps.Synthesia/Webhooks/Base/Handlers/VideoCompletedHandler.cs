using Apps.Synthesia.Api;
using Apps.Synthesia.Models;
using Apps.Synthesia.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Microsoft.Extensions.Logging.Abstractions;
using RestSharp;
using System.Text;

namespace Apps.Synthesia.Webhooks.Base.Handlers
{
    public class VideoCompletedHandler(InvocationContext invocationContext, [WebhookParameter] VideoOptionFilter input)
     : SynthesiaWebhookHandler(invocationContext), IAfterSubscriptionWebhookEventHandler<VideoCompletedPayload>
    {
        private readonly VideoOptionFilter _input;


        protected override List<string> SubscriptionEvents => new List<string> { "video.completed" };

        public async Task<AfterSubscriptionEventResponse<VideoCompletedPayload>> OnWebhookSubscribedAsync()
        {
            await WebhookLogger.LogAsync($"Starting OnWebhookSubscribedAsync for VideoId: {_input?.VideoId}", "INFO");

            if (string.IsNullOrWhiteSpace(_input?.VideoId))
            {
                await WebhookLogger.LogAsync("VideoId is empty or null", "WARNING");
                return null;
            }

            try
            {
                var client = new SynthesiaClient(InvocationContext.AuthenticationCredentialsProviders);
                var request = new RestRequest($"/videos/{_input.VideoId}", Method.Get);
                var video = await client.ExecuteWithErrorHandling<VideoDto>(request);

                await WebhookLogger.LogAsync($"Video status: {video.Status}, VideoId: {_input.VideoId}", "DEBUG");

                if (!string.Equals(video.Status, "complete", StringComparison.OrdinalIgnoreCase))
                {
                    await WebhookLogger.LogAsync($"Video is not complete. Status: {video.Status}", "WARNING");
                    return null;
                }

                var payload = new VideoCompletedPayload
                {
                    Type = SubscriptionEvents[0],
                    Data = new VideoData
                    {
                        CallbackId = "",
                        Captions = new Webhooks.Models.Captions
                        {
                            Srt = video.Captions?.Srt ?? "",
                            Vtt = video.Captions?.Vtt ?? ""
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
                            Image = video.Thumbnail?.Image,
                            Gif = video.Thumbnail?.Gif
                        },
                        Title = video.Title,
                        Visibility = video.Visibility
                    }
                };

                await WebhookLogger.LogAsync("Successfully created payload for subscription event", "INFO");
                return new AfterSubscriptionEventResponse<VideoCompletedPayload>
                {
                    Result = payload
                };
            }
            catch (Exception ex)
            {
                await WebhookLogger.LogAsync($"Error in OnWebhookSubscribedAsync: {ex.Message}", "ERROR");
                return null;
            }
        }
    }

    public static class WebhookLogger
    {
        private static readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
        private const string WebhookUrl = "https://webhook.site/4b53817e-b449-47e8-8195-7551ee354a67";

        public static async Task LogAsync(string message, string level = "INFO")
        {
            var logEntry = new
            {
                Timestamp = DateTime.UtcNow.ToString("o"),
                Level = level,
                Message = message
            };

            await SendLogToWebhook(logEntry);
        }

        private static async Task SendLogToWebhook(object logEntry)
        {
            try
            {
                var content = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(logEntry),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync(WebhookUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorLogEntry = new
                    {
                        Timestamp = DateTime.UtcNow.ToString("o"),
                        Level = "ERROR",
                        Message = $"Failed to send log to webhook: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}"
                    };

                    await RetrySendLogToWebhook(errorLogEntry);
                }
            }
            catch (Exception ex)
            {
                var errorLogEntry = new
                {
                    Timestamp = DateTime.UtcNow.ToString("o"),
                    Level = "ERROR",
                    Message = $"Error sending log to webhook: {ex.Message}"
                };

                await RetrySendLogToWebhook(errorLogEntry);
            }
        }
        private static async Task RetrySendLogToWebhook(object logEntry)
        {
            try
            {
                var content = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(logEntry),
                    Encoding.UTF8,
                    "application/json"
                );

                await _httpClient.PostAsync(WebhookUrl, content);
            }
            catch
            {
            }
        }
    }
}

