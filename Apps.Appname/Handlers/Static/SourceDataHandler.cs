using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Appname.Handlers.Static;
public class SourceDataHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData()
    {
        var conflictBehaviors = new List<DataSourceItem>()
        {
            new DataSourceItem("workspace", "Workspace"),
            new DataSourceItem("my_videos", "My videos"),
            new DataSourceItem("shared_with_me", "Shared with me"),
        };
        return conflictBehaviors;
    }
}
