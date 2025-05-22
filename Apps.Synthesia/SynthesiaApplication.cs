using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.Synthesia;

public class SynthesiaApplication : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.Multimedia];
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}