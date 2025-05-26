using Apps.Synthesia.Actions;
using Apps.Synthesia.Models;
using Blackbird.Applications.Sdk.Common.Files;
using Tests.Synthesia.Base;

namespace Tests.Synthesia;

[TestClass]
public class VideoActionsTests : TestBase
{
    [TestMethod]
    public async Task SearchVideos_IssSuccess()
    {
        var actions = new VideoActions(InvocationContext, FileManager);

        var response = await actions.SearchVideo(new SearchVideosRequest { Source = "" }, new LimitOptionsRequest { });

        foreach (var video in response.Videos)
        {
            Console.WriteLine($"{video.Id} - {video.Title} - {video.Description}");
        }

        Assert.IsNotNull(response);
    }



    [TestMethod]
    public async Task GetXliffVideo_IssSuccess()
    {
        var actions = new VideoActions(InvocationContext, FileManager);

        var response = await actions.GetVideoXliff(new SearchXliffVideoRequest { VideoId = "3df96ea1-dc28-45c5-a0e2-e946ce133e18" });

        Assert.IsNotNull(response);
    }


    [TestMethod]
    public async Task DownloadVideo_IssSuccess()
    {
        var actions = new VideoActions(InvocationContext, FileManager);

        var response = await actions.DownloadVideo(new SearchXliffVideoRequest { VideoId = "3df96ea1-dc28-45c5-a0e2-e946ce133e18" });

        Assert.IsNotNull(response);
    }


    [TestMethod]
    public async Task GetVideoCaptions_IssSuccess()
    {
        var actions = new VideoActions(InvocationContext, FileManager);

        var response = await actions.GetVideoCaptions(new SearchXliffVideoRequest { VideoId = "b57e26a1-64af-4a8a-83f8-2203728a5bd1" },
            new CaptionFormatRequest { CaptionFormat = ".srt" });

        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task UpdateXliffVideo_IssSuccess()
    {
        var actions = new VideoActions(InvocationContext, FileManager);

        var response = await actions.UploadTranslatedXliff(
            new UploadXliffRequest
            {
                VideoId = "3df96ea1-dc28-45c5-a0e2-e946ce133e18",
                XliffFile = new FileReference { Name = "test.xliff" }
            });

        Assert.IsNotNull(response);
    }


    [TestMethod]
    public async Task CreateVideo_IssSuccess()
    {
        var actions = new VideoActions(InvocationContext, FileManager);

        var response = await actions.CreateVideo(new CreateVideoRequest
        {
            Title = "Unit Test Video with minimum inputs",
            InputScriptTexts = new List<string> { "Hello, this is a test with minimum inputs!", "Hello 2, this is a test with minimum inputs!" },
        });

        Assert.IsNotNull(response);
    }

    [TestMethod]
    public async Task CreateVideoFromTemplate_IssSuccess()
    {
        var actions = new VideoActions(InvocationContext, FileManager);

        var response = await actions.CreateVideoFromTemplate(new CreateVideoFromTemplateRequest
        {
            TemplateId = "29748e74-ecfc-4a6c-b89d-15f7a7b1395f",
            Test = true,
        });

        Assert.IsNotNull(response);
    }
}
