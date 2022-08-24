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
            foreach (var t in masks)
            {
                GetComponent<RawImage>().texture = t;
                yield return new WaitForSeconds(intervalTime);
            }
        
            yield return null;
        }
        yield return null;
    }
}