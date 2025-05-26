using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Synthesia.Handlers.Static
{
    public class CaptionFormatDataHandler : IStaticDataSourceItemHandler
    {
        public IEnumerable<DataSourceItem> GetData()
        {
            var conflictBehaviors = new List<DataSourceItem>()
        {
            new DataSourceItem(".srt", "srt format"),
            new DataSourceItem(".vtt", "vtt format")
        };
            return conflictBehaviors;
        }
    }
}