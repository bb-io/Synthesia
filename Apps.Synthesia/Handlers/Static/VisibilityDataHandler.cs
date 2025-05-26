using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Synthesia.Handlers.Static
{
    public class VisibilityDataHandler : IStaticDataSourceItemHandler
    {
        public IEnumerable<DataSourceItem> GetData()
        {
            var conflictBehaviors = new List<DataSourceItem>()
        {
            new DataSourceItem("private", "Private"),
            new DataSourceItem("public", "Public"),
        };
            return conflictBehaviors;
        }
    }
}
