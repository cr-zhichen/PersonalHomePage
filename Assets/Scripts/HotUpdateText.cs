using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 文本热更新
/// </summary>
public class HotUpdateText : MonoBehaviour
{
    private Image BgImage => GameObject.Find("Bg").GetComponent<Image>();
    private Camera MainCamera => GameObject.Find("Main Camera").GetComponent<Camera>();

    private MaskCycle MaskCycle => GameObject.Find("MixedColor").GetComponent<MaskCycle>();

    private TextMeshProUGUI NameText => GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
    private TextMeshProUGUI Introduce => GameObject.Find("Introduce").GetComponent<TextMeshProUGUI>();

    private GameObject LinkPrefab => Resources.Load("Prefabs/Link") as GameObject;
    private GameObject LinkParent => GameObject.Find("Links");

    private GameObject ButtonPrefab => Resources.Load("Prefabs/Button") as GameObject;
    private GameObject ButtonParent => GameObject.Find("Buttons");

    private GameObject InformationPrefab => Resources.Load("Prefabs/Information") as GameObject;
    private GameObject InformationParent => GameObject.Find("Information");

    private async void Start()
    {
        var response = await HttpServiceEncapsulation.GetStreamingAssetsAsync("config.json");
        JObject json = JObject.Parse(response.DataAsText);

        List<Task> tasks = new List<Task>
        {
            //=============读取光标=============//
            LoadImage(json["Pointer"]?["NormalUrl"]?.ToString(), texture => GameManager.Instance.normalCursor = texture),
            LoadImage(json["Pointer"]?["SuspensionUrl"]?.ToString(), texture => GameManager.Instance.selectCursor = texture),
            //=============读取背景=============//
            LoadImage(json["Background"]?["ImageUrl"]?.ToString(), texture =>
            {
                BgImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                BgImage.enabled = true;
            })
        };

        ColorUtility.TryParseHtmlString(json["Background"]?["Color"]?.ToString(), out var bgColor);
        MainCamera.backgroundColor = bgColor;

        foreach (var maskUrl in json["Background"]?["MaskListUrl"] ?? new JArray())
        {
            tasks.Add(LoadImage(maskUrl.ToString(), texture =>
            {
                MaskCycle.masks.Add(texture);
                MaskCycle.gameObject.GetComponent<RawImage>().enabled = true;
            }));
        }

        MaskCycle.intervalTime = json["Background"]?["MaskIntervalTime"]?.ToObject<float>() ?? 0.1f;

        ColorUtility.TryParseHtmlString(json["Background"]?["Color"]?.ToString(), out var maskColor);
        MaskCycle.GetComponent<RawImage>().color = maskColor;

        //=============读取右侧装饰=============//
        for (int i = 0; i < 5; i++)
        {
            string index = i.ToString();
            tasks.Add(LoadImage(json["Decorate"]?[index]?["ImageUrl"]?.ToString(), texture =>
            {
                var rawImage = GameObject.Find($"R_{index}").GetComponent<RawImage>();
                rawImage.texture = texture;
                rawImage.GetComponent<FollowMouse>().mobileDistanceThan = (float)json["Decorate"]?[$"{index}"]?["MobileDistanceThan"];
                rawImage.enabled = true;
            }));
        }

        //=============读取文字=============//
        NameText.text = json["Name"]?.ToString();
        Introduce.text = json["Introduce"]?.ToString();

        foreach (var link in json["Links"] ?? new JArray())
        {
            var linkObject = Instantiate(LinkPrefab, LinkParent.transform);
            linkObject.GetComponent<ButtonURL>().url = link["Url"]?.ToString();
            linkObject.GetComponent<TextMeshProUGUI>().text = link["Text"]?.ToString();
        }

        //=============读取按钮=============//
        foreach (var button in json["Buttons"] ?? new JArray())
        {
            var buttonObject = Instantiate(ButtonPrefab, ButtonParent.transform);
            buttonObject.GetComponent<ButtonURL>().url = button["Url"]?.ToString();

            tasks.Add(LoadImage(button["ImageUrl"]?.ToString(), texture =>
            {
                buttonObject.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                buttonObject.GetComponent<Image>().enabled = true;
            }));

            tasks.Add(LoadImage(button["ImagePressUrl"]?.ToString(), texture =>
            {
                SpriteState state = new SpriteState
                {
                    highlightedSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f))
                };
                buttonObject.GetComponent<Button>().spriteState = state;
            }));
        }

        //=============读取下方信息=============//
        foreach (var information in json["Information"] ?? new JArray())
        {
            var informationObject = Instantiate(InformationPrefab, InformationParent.transform);
            informationObject.GetComponent<ButtonURL>().url = information["Url"]?.ToString();
            informationObject.GetComponent<TextMeshProUGUI>().text = information["Text"]?.ToString();
        }

        // 等待所有任务完成
        await Task.WhenAll(tasks);
    }

    /// <summary>
    /// 异步加载图片
    /// </summary>
    /// <param name="url"></param>
    /// <param name="onLoad"></param>
    /// <returns></returns>
    private async Task LoadImage(string url, System.Action<Texture2D> onLoad)
    {
        var response = await HttpServiceEncapsulation.GetStreamingAssetsAsync(url);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(response.Data);
        onLoad(texture);
    }
}
