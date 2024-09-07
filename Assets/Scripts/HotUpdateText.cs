using System.Collections;
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

    IEnumerator Start()
    {
        HttpServiceEncapsulation.GetStreamingAssets("config.json", (response =>
        {
            // !!!! 注意，在WebGL中使用Newtonsoft.Json对Json解析时，由于反射功能受限，无法使用 JsonConverter.Deserialize<T> 方法，必须使用 JObject.Parse 方法
            JObject json = JObject.Parse(response.DataAsText);

            //=============读取光标=============//
            HttpServiceEncapsulation.GetStreamingAssets(json["Pointer"]?["NormalUrl"]?.ToString(), (httpResponse =>
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(httpResponse.Data);
                GameManager.Instance.normalCursor = texture;
            }));

            HttpServiceEncapsulation.GetStreamingAssets(json["Pointer"]?["SuspensionUrl"]?.ToString(), (httpResponse =>
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(httpResponse.Data);
                GameManager.Instance.selectCursor = texture;
            }));

            //=============读取背景=============//
            HttpServiceEncapsulation.GetStreamingAssets(json["Background"]?["ImageUrl"]?.ToString(), (httpResponse =>
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(httpResponse.Data);
                BgImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                BgImage.enabled = true;
            }));

            ColorUtility.TryParseHtmlString(json["Background"]?["Color"]?.ToString(), out var bgColor);
            MainCamera.backgroundColor = bgColor;


            foreach (var maskUrl in json["Background"]?["MaskListUrl"] ?? new JArray())
            {
                HttpServiceEncapsulation.GetStreamingAssets(maskUrl.ToString(), (httpResponse =>
                {
                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(httpResponse.Data);
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
                HttpServiceEncapsulation.GetStreamingAssets(json["Decorate"]?[index]?["ImageUrl"]?.ToString(),
                    (httpResponse =>
                    {
                        Texture2D texture = new Texture2D(1, 1);
                        texture.LoadImage(httpResponse.Data);
                        GameObject.Find($"R_{index}").GetComponent<RawImage>().texture = texture;
                        GameObject.Find($"R_{index}").GetComponent<FollowMouse>().mobileDistanceThan = (float)json["Decorate"]?[$"{index}"]?["MobileDistanceThan"];
                        GameObject.Find($"R_{index}").GetComponent<RawImage>().enabled = true;
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
                HttpServiceEncapsulation.GetStreamingAssets(button["ImageUrl"]?.ToString(), (httpResponse =>
                {
                    var texture = new Texture2D(1, 1);
                    texture.LoadImage(httpResponse.Data);
                    buttonObject.GetComponent<Image>().sprite = Sprite.Create(texture,
                        new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    buttonObject.GetComponent<Image>().enabled = true;
                }));
                HttpServiceEncapsulation.GetStreamingAssets(button["ImagePressUrl"]?.ToString(), (httpResponse1 =>
                {
                    var texture1 = new Texture2D(1, 1);
                    texture1.LoadImage(httpResponse1.Data);

                    //设置变化状态
                    SpriteState state = new SpriteState
                    {
                        highlightedSprite = Sprite.Create(texture1, new Rect(0, 0, texture1.width, texture1.height), new Vector2(0.5f, 0.5f))
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
        }));
        yield return null;
    }
}
