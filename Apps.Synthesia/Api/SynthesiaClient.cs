using Apps.Synthesia.Constants;
using Apps.Synthesia.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Synthesia.Api;

public class SynthesiaClient : BlackBirdRestClient
{
    public SynthesiaClient(IEnumerable<AuthenticationCredentialsProvider> creds) : base(new()
    {
        BaseUrl = new Uri("https://api.synthesia.io/v2"),
        MaxTimeout = 180000,
    })
    {
        this.AddDefaultHeader("Authorization", creds.Get(CredsNames.Token).Value);
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {

        if (response == null)
        {
            return new PluginApplicationException($"Error: {response.ErrorMessage}");
        }

        if (string.IsNullOrEmpty(response.Content))
        {
            return new PluginApplicationException($"Error: {response.ErrorMessage}");
        }

        var responseContent = response.Content!;
        try
        {
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent, JsonSettings);
            if (errorResponse?.Error != null)
            {
                return new PluginApplicationException($"{errorResponse.Error} - {errorResponse.Context}");
            }
        }
        catch (Exception ex)
        {
            return new PluginApplicationException($"Error: {ex.Message}. Raw content: {responseContent}", ex);
        }

        return new PluginApplicationException($"Error: {responseContent}");
    }

    public override async Task<T> ExecuteWithErrorHandling<T>(RestRequest request)
    {
        string content = (await ExecuteWithErrorHandling(request)).Content;
        T val = JsonConvert.DeserializeObject<T>(content, JsonSettings);
        if (val == null)
        {
            throw new Exception($"Could not parse {content} to {typeof(T)}");
        }

        return val;
    }

}