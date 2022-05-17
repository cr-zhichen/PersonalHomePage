using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    public Texture2D cursor1;
    public Texture2D cursor2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Cursor.SetCursor(cursor1, Vector2.zero, CursorMode.Auto);
        
        oldPos = Input.mousePosition;
        newPos= Input.mousePosition;
    }
    
    public void ChangeCursor(int cursor)
    {
        if (cursor == 1)
        {
            Cursor.SetCursor(cursor1, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursor2, Vector2.zero, CursorMode.Auto);
        }
    }
    
    private Vector2 oldPos;
    private Vector2 newPos;

    public Vector2 delta;
    
    private void Update()
    {
        //获取鼠标坐标
        newPos = Input.mousePosition;
        delta = newPos - oldPos;
        oldPos = newPos;
    }

    
}
