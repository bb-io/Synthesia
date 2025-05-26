using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Synthesia.Handlers.Static
{
    public class AvatarHorizontalAlignDataHandler : IStaticDataSourceItemHandler
    {
        public IEnumerable<DataSourceItem> GetData()
        {
            var conflictBehaviors = new List<DataSourceItem>()
        {
            new DataSourceItem("left", "Left"),
            new DataSourceItem("center", "Center"),
            new DataSourceItem("right", "Right")
        };
            return conflictBehaviors;
        }
    }
}