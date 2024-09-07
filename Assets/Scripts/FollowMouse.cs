using UnityEngine;

/// <summary>
/// 鼠标跟随
/// </summary>
public class FollowMouse : MonoBehaviour
{
    /// <summary>
    /// 需要跟随鼠标的物体
    /// </summary>
    private GameObject _g;

    /// <summary>
    /// 需要跟随鼠标的物体的RectTransform
    /// </summary>
    private RectTransform _gRect;

    /// <summary>
    /// 鼠标上一帧的坐标
    /// </summary>
    private Vector2 _oldPos;

    /// <summary>
    /// 鼠标当前帧的坐标
    /// </summary>
    private Vector2 _newPos;

    /// <summary>
    /// 物体当前的坐标
    /// </summary>
    private Vector2 _thisPos;

    /// <summary>
    /// 移动距离比例
    /// </summary>
    public float mobileDistanceThan;

    private void Start()
    {
        _g = gameObject;
        _oldPos = Input.mousePosition;
        _newPos = Input.mousePosition;
        _thisPos = _g.GetComponent<RectTransform>().anchoredPosition;
        _gRect = _g.GetComponent<RectTransform>();
    }

    private void Update()
    {
        // 获取鼠标坐标
        _newPos = Input.mousePosition;

        var delta = (_newPos - _oldPos) / mobileDistanceThan;

        if (Mathf.Abs(delta.x) > 50 || Mathf.Abs(delta.y) > 50)
        {
            delta.x = 0;
            delta.y = 0;
        }

        if (delta is { x: 0, y: 0 })
        {
            Vector2 gPos = _gRect.anchoredPosition;
            Vector2 pos = Vector3.Lerp(gPos, _thisPos, Time.deltaTime);
            _gRect.anchoredPosition = pos;
        }
        else
        {
            // 将g移动到鼠标坐标
            var position = _g.transform.position;
            position = new Vector3(position.x + delta.x, position.y + delta.y, position.z);
            _g.transform.position = position;
        }

        _oldPos = _newPos;
    }
}
