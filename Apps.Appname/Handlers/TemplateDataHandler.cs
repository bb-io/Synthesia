using Apps.Appname;
using Apps.Synthesia.Models;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Synthesia.Handlers
{
    public class TemplateDataHandler : Invocable, IAsyncDataSourceItemHandler
    {
        public TemplateDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }
        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var restRequest = new RestRequest("templates", Method.Get);
            restRequest.AddQueryParameter("limit", "100");
            var response = await Client.ExecuteWithErrorHandling<ListTemplatesResponse>(restRequest);

            return response.Templates.Select(template => new DataSourceItem
            {
                Value = template.Id,
                DisplayName = template.Title
            });
        }
    }
}