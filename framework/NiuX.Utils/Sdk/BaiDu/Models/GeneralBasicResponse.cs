using Newtonsoft.Json;

namespace NiuX.Sdk.BaiDu.Models;

public class GeneralBasicResponse
{
    [JsonProperty("words_result")] public List<WordsResult> WordsResults { get; set; }

    [JsonProperty("words_result_num")] public int WordsResultNum { get; set; }

    [JsonProperty("log_id")] public long LogId { get; set; }

    public class WordsResult
    {
        public string Words { get; set; }
    }
}