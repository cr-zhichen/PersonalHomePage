using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 按钮跳转到指定URL
/// </summary>
public class ButtonURL : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string url;

    private void Start()
    {
        //监听按钮点击事件
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }
            Application.OpenURL(url);
        });
    }

    /// <summary>
    /// 鼠标进入
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("鼠标进入");
        if (string.IsNullOrEmpty(url))
        {
            return;
        }
        GameManager.Instance.ChangeCursor(GameManager.CursorType.Select);
    }

    /// <summary>
    /// 鼠标离开
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("鼠标离开");
        if (string.IsNullOrEmpty(url))
        {
            return;
        }
        GameManager.Instance.ChangeCursor(GameManager.CursorType.Normal);
    }
}
