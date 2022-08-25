using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using BestHTTP;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HotUpdateText : MonoBehaviour
{
    public Image bgImage => GameObject.Find("Bg").GetComponent<Image>();
    public Camera mainCamera => GameObject.Find("Main Camera").GetComponent<Camera>();

    public MaskCycle maskCycle => GameObject.Find("MixedColor").GetComponent<MaskCycle>();

    public TextMeshProUGUI nameText => GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
    public TextMeshProUGUI introduce => GameObject.Find("Introduce").GetComponent<TextMeshProUGUI>();

    public GameObject linkPrefab => Resources.Load("Prefabs/Link") as GameObject;
    public GameObject linkParent => GameObject.Find("Links");

    public GameObject buttonPrefab => Resources.Load("Prefabs/Button") as GameObject;
    public GameObject buttonParent => GameObject.Find("Buttons");


    public GameObject informationPrefab => Resources.Load("Prefabs/Information") as GameObject;
    public GameObject informationParent => GameObject.Find("Informations");

    IEnumerator Start()
    {
        HttpServiceEncapsulation.GetStreamingAssets("config.json", (response =>
        {
            JObject json = JObject.Parse(response.DataAsText);

            //=============读取光标=============//
            HttpServiceEncapsulation.GetStreamingAssets(json["Pointer"]?["NormalUrl"]?.ToString(), (httpResponse =>
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(httpResponse.Data);
                GameManager.instance.cursor1 = texture;
            }));

            HttpServiceEncapsulation.GetStreamingAssets(json["Pointer"]?["SuspensionUrl"]?.ToString(), (httpResponse =>
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(httpResponse.Data);
                GameManager.instance.cursor2 = texture;
            }));

            //=============读取背景=============//
            HttpServiceEncapsulation.GetStreamingAssets(json["Background"]?["ImageUrl"]?.ToString(), (httpResponse =>
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(httpResponse.Data);
                bgImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                bgImage.enabled = true;
            }));

            ColorUtility.TryParseHtmlString(json["Background"]?["Color"]?.ToString(), out var bgColor);
            mainCamera.backgroundColor = bgColor;


            foreach (var maskUrl in json["Background"]?["MaskListUrl"] ?? new JArray())
            {
                HttpServiceEncapsulation.GetStreamingAssets(maskUrl.ToString(), (httpResponse =>
                {
                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(httpResponse.Data);
                    maskCycle.masks.Add(texture);
                    maskCycle.gameObject.GetComponent<RawImage>().enabled = true;
                }));
            }

            maskCycle.intervalTime = json["Background"]?["MaskIntervalTime"]?.ToObject<float>() ?? 0.1f;

            ColorUtility.TryParseHtmlString(json["Background"]?["Color"]?.ToString(), out var maskColor);
            maskCycle.GetComponent<RawImage>().color = maskColor;

            //=============读取右侧装饰=============//

            for (int i = 0; i < 5; i++)
            {
                string index = i.ToString();
                HttpServiceEncapsulation.GetStreamingAssets(json["Decorate"]?[index]?["ImageUrl"]?.ToString(),
                    (httpResponse =>
                    {
                        Texture2D texture = new Texture2D(1, 1);
                        texture.LoadImage(httpResponse.Data);
                        GameObject.Find($"R{index}").GetComponent<RawImage>().texture = texture;
                        GameObject.Find($"R{index}").GetComponent<FollowMouse>().mobileDistanceThan =
                            (float)json["Decorate"]?[$"{index}"]?["MobileDistanceThan"];

                        GameObject.Find($"R{index}").GetComponent<RawImage>().enabled = true;
                    }));
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
                    SpriteState state = new SpriteState();
                    state.highlightedSprite = Sprite.Create(texture1,
                        new Rect(0, 0, texture1.width, texture1.height), new Vector2(0.5f, 0.5f));

                    buttonObject.GetComponent<Button>().spriteState = state;
                }));
            }

            //=============读取下方信息=============//
            foreach (var information in json["Informations"] ?? new JArray())
            {
                var informationObject = Instantiate(informationPrefab, informationParent.transform);
                informationObject.GetComponent<ButtonURL>().url = information["Url"]?.ToString();
                informationObject.GetComponent<TextMeshProUGUI>().text = information["Text"]?.ToString();
            }
        }));
        yield return null;
    }
}