using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Mask循环显示
/// </summary>
public class MaskCycle : MonoBehaviour
{
    /// <summary>
    /// 需要循环显示的mask
    /// </summary>
    public List<Texture> masks = new List<Texture>();

    /// <summary>
    /// 间隔时间
    /// </summary>
    public float intervalTime = 0.1f;

    /// <summary>
    /// RawImage
    /// </summary>
    private RawImage _rawImage;


    private void Start()
    {
        _rawImage = GetComponent<RawImage>();
        StartCoroutine(CircularDisplay());
    }

    IEnumerator CircularDisplay()
    {
        //无限遍历masks
        while (true)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < masks.Count; i++)
            {
                _rawImage.texture = masks[i];
                yield return new WaitForSeconds(intervalTime);
            }

            yield return null;
        }
    }
}
