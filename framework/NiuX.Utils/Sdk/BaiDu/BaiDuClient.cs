using RestSharp;
using System.Text.Json;

namespace NiuX.Sdk.BaiDu;

public abstract class BaiDuClient
{
    public string AccesToken { get; set; } = "24.97b4dafe3ea8fd52f89ba99f84e4c8f5.2592000.1654065319.282335-25423130";

    /// <summary>
    /// 执行 Post 请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="restRequestAction"></param>
    /// <returns></returns>
    public async Task<T> ExecutePostAsync<T>(string url, Action<RestRequest> restRequestAction)
    {
        var client = new RestClient($"{url}?access_token={AccesToken}");
        var restRequest = new RestRequest();

        restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        restRequestAction(restRequest);

        return (await client.ExecutePostAsync(restRequest)).Content!.FromJson<T>();
    }
}