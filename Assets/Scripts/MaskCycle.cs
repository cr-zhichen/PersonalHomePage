using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskCycle : MonoBehaviour
{
    public List<Texture> masks = new();
    public float intervalTime = 0.1f;


    private void Start()
    {
        StartCoroutine(CircularDisplay());
    }

    IEnumerator CircularDisplay()
    {
        //无限遍历masks
        while (true)
        {
            for (int i = 0; i < masks.Count; i++)
            {
                GetComponent<RawImage>().texture = masks[i];
                yield return new WaitForSeconds(intervalTime);
            }

            yield return null;
        }

        yield return null;
    }
}