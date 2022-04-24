using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonURL : MonoBehaviour
{
    public string url;

    private void Start()
    {
        //监听按钮点击事件
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.OpenURL(url);
        });
    }

}
