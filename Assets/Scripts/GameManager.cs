using UnityEngine;

/// <summary>
/// 游戏管理
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 单例
    /// </summary>
    public static GameManager Instance;

    /// <summary>
    /// 光标纹理
    /// </summary>
    [HideInInspector]
    public Texture2D normalCursor;

    /// <summary>
    /// 光标纹理
    /// </summary>
    [HideInInspector]
    public Texture2D selectCursor;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    public void ChangeCursor(CursorType cursor)
    {
        switch (cursor)
        {
            case CursorType.Normal:
                Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
                break;
            case CursorType.Select:
                Cursor.SetCursor(selectCursor, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    /// <summary>
    /// 光标类型
    /// </summary>
    public enum CursorType
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal,

        /// <summary>
        /// 选择
        /// </summary>
        Select,
    }
}
