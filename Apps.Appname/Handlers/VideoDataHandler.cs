using Apps.Appname.Actions;
using Apps.Appname.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Appname.Handlers
{
    public class VideoDataHandler : Invocable, IAsyncDataSourceItemHandler
    {
        public VideoDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }
        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var restRequest = new RestRequest("videos", Method.Get);
            var response = await Client.ExecuteWithErrorHandling<ListVideosResponse>(restRequest);

            return response.Videos.Select(video => new DataSourceItem
            {
                Value = video.Id,
                DisplayName = video.Title
            });
        }
    }
}
