using Apps.Synthesia.Models;
using Apps.Synthesia.Webhooks.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using RestSharp;

namespace Apps.Synthesia.Webhooks
{
    [PollingEventList]
    public class PollingList : Invocable
    {
        public PollingList(InvocationContext invocationContext) : base(invocationContext)
        {
        }
        [PollingEvent("On video completed (Polling)", Description = "Triggers on any video completed")]
        public async Task<PollingEventResponse<DateMemory, Video>> OnVideoCompleted(PollingEventRequest<DateMemory> request,
            [PollingEventParameter] VideoFilter input)
        {
            var allVideos = new List<Video>();

            const int pageSize = 100;
            int currentOffset = 0;
            int? nextOffset = null;

            do
            {
                var restRequest = new RestRequest("videos", Method.Get);
                restRequest.AddQueryParameter("limit", pageSize.ToString());
                restRequest.AddQueryParameter("offset", currentOffset.ToString());

                var listResponse = await Client.ExecuteWithErrorHandling<ListWebhookVideosResponse>(restRequest);
                if (listResponse.Videos != null)
                {
                    allVideos.AddRange(listResponse.Videos);
                }

                nextOffset = listResponse.NextOffset;
                if (nextOffset.HasValue && nextOffset.Value > 0)
                {
                    currentOffset = nextOffset.Value;
                }
                else
                {
                    nextOffset = null;
                }
            }
            while (nextOffset.HasValue);


            var completedVideos = allVideos
                .Where(v => string.Equals(v.Status, "complete", StringComparison.OrdinalIgnoreCase))
                .Where(v => string.Equals(v.Id, input.VideoId, StringComparison.OrdinalIgnoreCase))
                .ToList();
                    

            if (!completedVideos.Any())
            {
                if (request.Memory == null)
                {
                    return new PollingEventResponse<DateMemory, Video>
                    {
                        FlyBird = false,
                        Memory = new DateMemory { LastInteractionDate = DateTime.UtcNow }
                    };
                }

                return new PollingEventResponse<DateMemory, Video>
                {
                    FlyBird = false,
                    Memory = request.Memory
                };
            }

            var withDates = completedVideos
                .Select(v =>
                {
                    var dt = DateTimeOffset
                                .FromUnixTimeSeconds(v.LastUpdatedAt)
                                .UtcDateTime;
                    return (Video: v, UpdatedAt: dt);
                })
                .ToList();

            if (request.Memory == null)
            {
                var maxDate = withDates.Max(x => x.UpdatedAt);
                return new PollingEventResponse<DateMemory, Video>
                {
                    FlyBird = false,
                    Memory = new DateMemory { LastInteractionDate = maxDate }
                };
            }

            var newVideos = withDates
                .Where(x => x.UpdatedAt > request.Memory.LastInteractionDate)
                .Select(x => x.Video)
                .ToList();

            if (!newVideos.Any())
            {
                return new PollingEventResponse<DateMemory, Video>
                {
                    FlyBird = false,
                    Memory = request.Memory
                };
            }

            var newMaxDate = withDates.Max(x => x.UpdatedAt);
            request.Memory.LastInteractionDate = newMaxDate;

            return new PollingEventResponse<DateMemory, Video>
            {
                FlyBird = true,
                Memory = request.Memory,
                Result = newVideos.FirstOrDefault()
            };
        }
    }
}
