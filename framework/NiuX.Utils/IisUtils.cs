using Microsoft.Web.Administration;

namespace NiuX;

public static class IisUtils
{
    /// <summary>
    /// 关闭其它站点，只开启输入名称的站点
    /// </summary>
    public static void StartWebsite(string siteName)
    {
        if (siteName.IsNullOrWhiteSpace()) return;

        using var webManager = new ServerManager();
        var first = webManager.Sites.FirstOrDefault(x => x.Name == siteName);
        first?.Start();
    }
}