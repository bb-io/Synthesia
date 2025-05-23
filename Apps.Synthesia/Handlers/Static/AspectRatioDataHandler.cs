using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Synthesia.Handlers.Static
{
    internal class AspectRatioDataHandler : IStaticDataSourceItemHandler
    {
        public IEnumerable<DataSourceItem> GetData()
        {
            var conflictBehaviors = new List<DataSourceItem>()
        {
            new DataSourceItem("16:9", "16:9"),
            new DataSourceItem("9:16", "9:16"),
            new DataSourceItem("1:1", "1:1"),
            new DataSourceItem("4:5", "4:5"),
            new DataSourceItem("5:4", "5:4")
        };
            return conflictBehaviors;
        }
    }
}