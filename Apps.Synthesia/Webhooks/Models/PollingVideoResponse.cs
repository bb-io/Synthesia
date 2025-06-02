using Apps.Synthesia.Models;

namespace Apps.Synthesia.Webhooks.Models
{
    public class PollingVideoResponse
    {
        public IEnumerable<Video> Videos { get; }

        public PollingVideoResponse(IEnumerable<Video> videos)
        {
            Videos = videos;
        }
    }
}
