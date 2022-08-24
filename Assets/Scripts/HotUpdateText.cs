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

public class HotUpdateText : MonoBehaviour
{
    public TextMeshProUGUI nameText => GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
    public TextMeshProUGUI introduce => GameObject.Find("Introduce").GetComponent<TextMeshProUGUI>();
    public GameObject linkPrefab => Resources.Load("Prefabs/Link") as GameObject;
    public GameObject linkParent => GameObject.Find("Links");
    public ButtonURL eMailUrl => GameObject.Find("E-Mail").GetComponent<ButtonURL>();
    public ButtonURL githubUrl => GameObject.Find("Github").GetComponent<ButtonURL>();
    public GameObject informationPrefab => Resources.Load("Prefabs/Information") as GameObject;
    public GameObject informationParent => GameObject.Find("Informations");

    private void Awake()
    {
        HTTPRequest request = new HTTPRequest(
            new Uri(Path.Combine(Application.streamingAssetsPath, "config.json")), HTTPMethods.Get,
            (originalRequest, response) =>
            {
                JObject json = JObject.Parse(response.DataAsText);

                nameText.text = json["Name"]?.ToString();
                introduce.text = json["Introduce"]?.ToString();
                foreach (var link in json["Links"])
                {
                    var linkObject = Instantiate(linkPrefab, linkParent.transform);
                    linkObject.GetComponent<ButtonURL>().url = link["Url"]?.ToString();
                    linkObject.GetComponent<TextMeshProUGUI>().text = link["Text"]?.ToString();
                }

                eMailUrl.url = json["EMail"]?.ToString();
                githubUrl.url = json["Github"]?.ToString();
                foreach (var information in json["Informations"])
                {
                    var informationObject = Instantiate(informationPrefab, informationParent.transform);
                    informationObject.GetComponent<ButtonURL>().url = information["Url"]?.ToString();
                    informationObject.GetComponent<TextMeshProUGUI>().text = information["Text"]?.ToString();
                }
            }).Send();
    }
}