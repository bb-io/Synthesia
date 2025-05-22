using Apps.Synthesia.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Utilities;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;
using System.Text;

namespace Apps.Synthesia.Actions;

[ActionList]
public class VideoActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : Invocable(invocationContext)
{
    [Action("Search videos", Description = "Searches videos")]
    public async Task<ListVideosResponse> SearchVideo([ActionParameter] SearchVideosRequest request,
        [ActionParameter] LimitOptionsRequest options)
    {
        var restRequest = new RestRequest("videos", Method.Get);

        restRequest.AddQueryParameter("limit", options.Limit?.ToString() ?? "100");
        restRequest.AddQueryParameter("offset", options.Offset?.ToString() ?? "0");

        if (!string.IsNullOrEmpty(request.Source))
            restRequest.AddQueryParameter("source", request.Source!);

        var response = await Client.ExecuteWithErrorHandling<ListVideosResponse>(restRequest);
        return response;
    }


    [Action("Get XLIFF content of video", Description = "Downloads XLIFF content file of video")]
    public async Task<DownloadXliffResponse> GetVideoXliff([ActionParameter] SearchXliffVideoRequest request)
    {
        var restRequest = new RestRequest($"/videos/{request.VideoId}/xliff", Method.Get);
        restRequest.AddQueryParameter("xliffVersion", "1.2");

        var xml = await Client.ExecuteWithErrorHandling(restRequest);

        var bytes = Encoding.UTF8.GetBytes(xml.ToString());
        using var stream = new MemoryStream(bytes);

        var filename = $"{request.VideoId}.xliff";
        var contentType = "application/xml";
        var fileRef = await fileManagementClient.UploadAsync(stream, contentType, filename);

        return new DownloadXliffResponse
        {
            File = fileRef
        };
    }


    [Action("Download video", Description = "Downloads video by its ID")]
    public async Task<FileResponse> DownloadVideo([ActionParameter] SearchXliffVideoRequest request)
    {

        var restRequest = new RestRequest($"/videos/{request.VideoId}", Method.Get);

        var video = await Client.ExecuteWithErrorHandling<Video>(restRequest);

        var fileContent = await FileDownloader.DownloadFileBytes(video.Download);

        var uri = new Uri(video.Download);
        var ext = Path.GetExtension(uri.AbsolutePath);
        if (string.IsNullOrEmpty(ext)) ext = ".mp4";

        var safeTitle = string.Concat(video.Title.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
        var filename = $"{safeTitle}{ext}";

        var contentType = "video/mp4";

        var fileRef = await fileManagementClient.UploadAsync(fileContent.FileStream, contentType, filename);

        return new FileResponse { File = fileRef };
    }


    [Action("Get video captions", Description = "Gets video captions by it`s ID")]
    public async Task<FileResponse> GetVideoCaptions([ActionParameter] SearchXliffVideoRequest request,
        [ActionParameter] CaptionFormatRequest format)
    {
        var metaReq = new RestRequest($"/videos/{request.VideoId}", Method.Get);
        var video = await Client.ExecuteWithErrorHandling<Video>(metaReq);

        var ext = format.CaptionFormat?.ToLowerInvariant() ?? ".srt";
        string? url = ext == ".srt"
            ? video.Captions?.Srt
            : video.Captions?.Vtt;

        if (string.IsNullOrEmpty(url))
            throw new PluginApplicationException($"No captions available in '{ext}' format for video '{request.VideoId}'.");

        var fileContent = await FileDownloader.DownloadFileBytes(url);

        var safeTitle = string.Concat(video.Title.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
        var filename = $"{safeTitle}{ext}";

        var contentType = ext == ".srt" ? "application/x-subrip" : "text/vtt";

        var fileRef = await fileManagementClient.UploadAsync(fileContent.FileStream, contentType, filename);

        return new FileResponse { File = fileRef };
    }

    [Action("Update video content from XLIFF", Description = "Uploads translated XLIFF to video")]
    public async Task<UploadXliffResponse> UploadTranslatedXliff(
            [ActionParameter] UploadXliffRequest request
        )
    {
        using var downloadedStream = await fileManagementClient.DownloadAsync(request.XliffFile);
        using var ms = new MemoryStream();
        await downloadedStream.CopyToAsync(ms);

        var xliffContent = Encoding.UTF8.GetString(ms.ToArray());

        var restRequest = new RestRequest("translate/manual", Method.Post)
            .AddJsonBody(new
            {
                videoId = request.VideoId,
                xliffContent = xliffContent,
                callbackId = request.CallbackId ?? ""
            });

        var response = await Client.ExecuteWithErrorHandling<UploadXliffResponse>(restRequest);

        if (string.IsNullOrEmpty(response.TranslatedVideoId))
            throw new PluginApplicationException("Failed to create translated video.");

        return response;
    }


    [Action("Create video", Description = "Creates a video by your input info")]
    public async Task<CreateVideoResponse> CreateVideo([ActionParameter] CreateVideoRequest request)
    {
        var clips = new List<object>();
        var count = request.InputScriptTexts.Count;
        for (int i = 0; i < count; i++)
        {
            clips.Add(new
            {
                scriptText = request.InputScriptTexts[i],
                avatar = request.InputAvatars[i],
                avatarSettings = new
                {
                    horizontalAlign = request.InputAvatarSettingsHorizontalAligns[i],
                    scale = request.InputAvatarSettingsScales[i],
                    style = request.InputAvatarSettingsStyles[i],
                    seamless = request.InputAvatarSettingsSeameless[i]
                },
                background = request.InputBackgrounds[i],
                backgroundSettings = new
                {
                    videoSettings = new
                    {
                        shortBackgroundContentMatchMode = request.InputBgShortMatchModes[i],
                        longBackgroundContentMatchMode = request.InputBgLongMatchModes[i]
                    }
                }
            });
        }

        var body = new Dictionary<string, object>
        {
            ["test"] = request.Test,
            ["title"] = request.Title ?? "My created video",
            ["input"] = clips
        };

        if (!string.IsNullOrWhiteSpace(request.Description))
            body["description"] = request.Description;
        if (!string.IsNullOrWhiteSpace(request.Visibility))
            body["visibility"] = request.Visibility;
        if (!string.IsNullOrWhiteSpace(request.AspectRatio))
            body["aspectRatio"] = request.AspectRatio;

        var restRequest = new RestRequest("videos", Method.Post)
            .AddJsonBody(body);

        var response = await Client.ExecuteWithErrorHandling<CreateVideoResponse>(restRequest);
        return response;
    }


    [Action("Create video from template", Description = "Creates a video based on a Synthesia template")]
    public async Task<CreateVideoResponse> CreateVideoFromTemplate(
            [ActionParameter] CreateVideoFromTemplateRequest request
        )
    {
        var keys = request.TemplateDataKeys.ToList();
        var values = request.TemplateDataValues.ToList();
        if (keys.Count != values.Count)
            throw new PluginMisconfigurationException("The number of TemplateDataKeys must match TemplateDataValues.");

        var templateData = new Dictionary<string, object>();
        for (int i = 0; i < keys.Count; i++)
            templateData[keys[i]] = values[i];


        var body = new Dictionary<string, object>
        {
            ["test"] = request.Test,
            ["templateId"] = request.TemplateId,
            ["templateData"] = templateData
        };

        if (!string.IsNullOrWhiteSpace(request.Visibility))
            body["visibility"] = request.Visibility!;
        if (!string.IsNullOrWhiteSpace(request.CallbackId))
            body["callbackId"] = request.CallbackId!;
        if (!string.IsNullOrWhiteSpace(request.Title))
            body["title"] = request.Title!;
        if (!string.IsNullOrWhiteSpace(request.Description))
            body["description"] = request.Description!;
        if (!string.IsNullOrWhiteSpace(request.CtaLabel) &&
            !string.IsNullOrWhiteSpace(request.CtaUrl))
        {
            body["ctaSettings"] = new
            {
                label = request.CtaLabel,
                url = request.CtaUrl
            };
        }

        var restRequest = new RestRequest("videos/fromTemplate", Method.Post)
            .AddJsonBody(body);

        var response = await Client.ExecuteWithErrorHandling<CreateVideoResponse>(restRequest);

        return response;
    }

}