using System.Text.Json;
using RestSharp;

namespace NiuX.Utils;

public class RestSharpHelper
{
    public static Task ExecuteGetAsync(string url, Dictionary<string, string> headers)
    {
        var client = new RestClient(url);

        var request = new RestRequest();

        foreach (var header in headers) request.AddHeader(header.Key, header.Value);

        return client.ExecuteGetAsync(request);
    }

    public static Task<RestResponse<T>> ExecuteGetAsync<T>(string url, Dictionary<string, string> headers)
    {
        var client = new RestClient(url);

        var request = new RestRequest();

        foreach (var header in headers) request.AddHeader(header.Key, header.Value);

        return client.ExecuteGetAsync<T>(request);
    }

    public static async Task<T?> ExecuteGetResponsDataAsync<T>(string url, Dictionary<string, string> headers)
    {
        var client = new RestClient(url);

        var request = new RestRequest();

        foreach (var header in headers) request.AddHeader(header.Key, header.Value);

        var response = await client.ExecuteGetAsync(request);
        return response.Content.IsNullOrEmpty() ? default : response.Content!.FromJson<T>();
    }

    public static Task<RestResponse<T>> ExecutePostAsync<T>(string url, object body)
    {
        var client = new RestClient(url);

        var request = new RestRequest();
        request.AddBody(body);

        return client.ExecutePostAsync<T>(request);
    }
}