using Apps.Synthesia.Handlers;
using Apps.Synthesia.Handlers.Static;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Tests.Synthesia.Base;

namespace Tests.Synthesia;

[TestClass]
public class DataHandlerTests : TestBase
{
    [TestMethod]
    public async Task VideoDataHandler_works()
    {
        var handler = new VideoDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod]
    public async Task TemplateDataHandler_works()
    {
        var handler = new TemplateDataHandler(InvocationContext);

        var result = await handler.GetDataAsync(new DataSourceContext { }, CancellationToken.None);

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod]
    public async Task AvatarDataHandler_works()
    {
        var handler = new AvatarDataHandler();

        var result =  handler.GetData();

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }
        Assert.IsTrue(result.Count() > 0);
    }

    [TestMethod]
    public async Task BackgroundDataHandler_works()
    {
        var handler = new BackgroundDataHandler();

        var result =handler.GetData();

        Console.WriteLine($"Total: {result.Count()}");
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}: {item.DisplayName}");
        }
        Assert.IsTrue(result.Count() > 0);
    }
}
