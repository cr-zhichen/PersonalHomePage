using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowMouse : MonoBehaviour
{

    private GameObject g;

    private Vector2 oldPos;
    private Vector2 newPos;
    
    private Vector2 thisPos;

    public Vector2 maxDistance;//最大移动距离
    [FormerlySerializedAs("speed")] public float mobileDistanceThan=20;

    private void Start()
    {
        g = gameObject;
        oldPos = Input.mousePosition;
        newPos= Input.mousePosition;
        thisPos = g.transform.position;
        
        //将thisPos转换为世界坐标
        thisPos = Camera.main.ScreenToWorldPoint(thisPos);

    }

    private void Update()
    {
        //获取鼠标坐标
        newPos = Input.mousePosition;
        
        var delta = (newPos - oldPos)/mobileDistanceThan;
        
        if (delta.x==0&&delta.y==0)
        {
            Vector2 gPos = Camera.main.ScreenToWorldPoint(g.transform.position);
            Vector2 worldPos = Vector3.Lerp(gPos, thisPos, Time.deltaTime);
            //将g的位置缓慢移动到thisPos
            g.transform.position= Camera.main.WorldToScreenPoint(worldPos);
                
        }
        else
        {
            //将g移动到鼠标坐标
            var position = g.transform.position;
            position = new Vector3(position.x+delta.x, position.y+delta.y, position.z);
            g.transform.position = position;
        }

        oldPos=newPos;
    }

}
