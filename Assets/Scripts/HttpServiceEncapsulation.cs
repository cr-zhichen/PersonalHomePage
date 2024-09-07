using System;
using System.IO;
using System.Threading.Tasks;
using Best.HTTP;
using UnityEngine;

/// <summary>
/// Http服务封装
/// </summary>
public static class HttpServiceEncapsulation
{
    /// <summary>
    /// 获取StreamingAssets目录下的文件
    /// </summary>
    /// <param name="url"></param>
    /// <param name="response"></param>
    public static void GetStreamingAssets(string url, Action<HTTPResponse> response)
    {
        if (string.IsNullOrEmpty(url))
        {
            return;
        }

        HTTPRequest.CreateGet(new Uri(Path.Combine(Application.streamingAssetsPath, url)), (_, httpResponse) => { response?.Invoke(httpResponse); }).Send();
    }


    /// <summary>
    /// 获取StreamingAssets目录下的文件
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<HTTPResponse> GetStreamingAssetsAsync(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return null;
        }

        var response = new HTTPRequest(new Uri(Path.Combine(Application.streamingAssetsPath, url)));

        return await response.GetHTTPResponseAsync();
    }
}
