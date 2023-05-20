using NiuX.Sdk.BaiDu.Models;
using RestSharp;

namespace NiuX.Sdk.BaiDu;

public class OcrClient : BaiDuClient
{
    /// <summary>
    /// 通用
    /// </summary>
    /// <returns></returns>
    public Task<GeneralBasicResponse> GeneralBasic(GeneralBasicRequest request)
    {
        return ExecutePostAsync<GeneralBasicResponse>("https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic",
            x => x.AddParameter("image", request.Base64EncodedString));
    }
}