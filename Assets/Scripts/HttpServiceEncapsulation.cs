using System;
using System.IO;
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
}
