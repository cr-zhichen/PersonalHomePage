using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BestHTTP;
using UnityEngine;

public static class HttpServiceEncapsulation
{
    /// <summary>
    /// 获取StreamingAssets目录下的文件
    /// </summary>
    /// <param name="url"></param>
    /// <param name="response"></param>
    public static void GetStreamingAssets(string url, Action<HTTPResponse> response)
    {
        if (url.Length == 0)
        {
            return;
        }

        HTTPRequest request = new HTTPRequest(
            new Uri(Path.Combine(Application.streamingAssetsPath, url)), HTTPMethods.Get,
            (originalRequest, _response) => { response?.Invoke(_response); }).Send();
    }
    
}