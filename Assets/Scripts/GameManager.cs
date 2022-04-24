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

    
}
