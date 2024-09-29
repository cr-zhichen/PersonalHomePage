using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 文本热更新
/// </summary>
public class HotUpdateText : MonoBehaviour
{
    [SerializeField]
    private Image bgImage;
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private MaskCycle maskCycle;

    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI introduce;

    [SerializeField]
    private GameObject linkPrefab;
    [SerializeField]
    private GameObject linkParent;

    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private GameObject buttonParent;

    [SerializeField]
    private GameObject informationPrefab;
    [SerializeField]
    private GameObject informationParent;


    private async void Start()
    {
        var response = await HttpServiceEncapsulation.GetStreamingAssetsAsync("config.json");
        JObject json = JObject.Parse(response.DataAsText);

        //=============读取光标=============//
        LoadImage(json["Pointer"]?["NormalUrl"]?.ToString(), texture => GameManager.Instance.normalCursor = texture).Forget();
        LoadImage(json["Pointer"]?["SuspensionUrl"]?.ToString(), texture => GameManager.Instance.selectCursor = texture).Forget();
        //=============读取背景=============//
        LoadImage(json["Background"]?["ImageUrl"]?.ToString(), texture =>
        {
            bgImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            bgImage.enabled = true;
        }).Forget();

        ColorUtility.TryParseHtmlString(json["Background"]?["Color"]?.ToString(), out var bgColor);
        mainCamera.backgroundColor = bgColor;

        foreach (var maskUrl in json["Background"]?["MaskListUrl"] ?? new JArray())
        {
            LoadImage(maskUrl.ToString(), texture =>
            {
                maskCycle.masks.Add(texture);
                maskCycle.gameObject.GetComponent<RawImage>().enabled = true;
            }).Forget();
        }

        maskCycle.intervalTime = json["Background"]?["MaskIntervalTime"]?.ToObject<float>() ?? 0.1f;

        ColorUtility.TryParseHtmlString(json["Background"]?["Color"]?.ToString(), out var maskColor);
        maskCycle.GetComponent<RawImage>().color = maskColor;

        //=============读取右侧装饰=============//
        for (int i = 0; i < 5; i++)
        {
            string index = i.ToString();
            LoadImage(json["Decorate"]?[index]?["ImageUrl"]?.ToString(), texture =>
            {
                var rawImage = GameObject.Find($"R_{index}").GetComponent<RawImage>();
                rawImage.texture = texture;
                rawImage.GetComponent<FollowMouse>().mobileDistanceThan = (float)json["Decorate"]?[$"{index}"]?["MobileDistanceThan"];
                rawImage.enabled = true;
            }).Forget();
        }

        //=============读取文字=============//
        nameText.text = json["Name"]?.ToString();
        introduce.text = json["Introduce"]?.ToString();

        foreach (var link in json["Links"] ?? new JArray())
        {
            var linkObject = Instantiate(linkPrefab, linkParent.transform);
            linkObject.GetComponent<ButtonURL>().url = link["Url"]?.ToString();
            linkObject.GetComponent<TextMeshProUGUI>().text = link["Text"]?.ToString();
        }

        //=============读取按钮=============//
        foreach (var button in json["Buttons"] ?? new JArray())
        {
            var buttonObject = Instantiate(buttonPrefab, buttonParent.transform);
            buttonObject.GetComponent<ButtonURL>().url = button["Url"]?.ToString();

            LoadImage(button["ImageUrl"]?.ToString(), texture =>
            {
                buttonObject.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                buttonObject.GetComponent<Image>().enabled = true;
            }).Forget();

            LoadImage(button["ImagePressUrl"]?.ToString(), texture =>
            {
                SpriteState state = new SpriteState
                {
                    highlightedSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f))
                };
                buttonObject.GetComponent<Button>().spriteState = state;
            }).Forget();
        }

        //=============读取下方信息=============//
        foreach (var information in json["Information"] ?? new JArray())
        {
            var informationObject = Instantiate(informationPrefab, informationParent.transform);
            informationObject.GetComponent<ButtonURL>().url = information["Url"]?.ToString();
            informationObject.GetComponent<TextMeshProUGUI>().text = information["Text"]?.ToString();
        }
    }

    /// <summary>
    /// 异步加载图片
    /// </summary>
    /// <param name="url"></param>
    /// <param name="onLoad"></param>
    /// <returns></returns>
    private async UniTask LoadImage(string url, System.Action<Texture2D> onLoad)
    {
        var response = await HttpServiceEncapsulation.GetStreamingAssetsAsync(url);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(response.Data);
        onLoad(texture);
    }
}
