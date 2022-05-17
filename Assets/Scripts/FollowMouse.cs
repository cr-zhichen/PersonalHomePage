using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowMouse : MonoBehaviour
{

    private GameObject g;

    private Vector2 thisPos;
    
    [FormerlySerializedAs("speed")] public float mobileDistanceThan=20;

    private void Start()
    {
        g = gameObject;
        thisPos = g.GetComponent<RectTransform>().anchoredPosition;

    }
    
    private void Update()
    {
        var delta = GameManager.instance.delta/mobileDistanceThan;
        
        if (Mathf.Abs(delta.x)>50||Mathf.Abs(delta.y)>50)
        {
            delta.x = 0;
            delta.y = 0;
        }

        if (delta.x==0&&delta.y==0)
        {
            Vector2 gPos = g.GetComponent<RectTransform>().anchoredPosition;
            Vector2 pos = Vector3.Lerp(gPos, thisPos, Time.deltaTime);
            g.GetComponent<RectTransform>().anchoredPosition=pos;
                
        }
        else
        {
            //将g移动到鼠标坐标
            var position = g.transform.position;
            position = new Vector3(position.x+delta.x, position.y+delta.y, position.z);
            g.transform.position = position;
        }
    }

}
