using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonURL : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
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
    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("鼠标进入");
        GameManager.instance.ChangeCursor(2);
    }
    //鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("鼠标离开");
        GameManager.instance.ChangeCursor(1);
    }



}
