using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ModelFollowMouse : MonoBehaviour
{

    private GameObject g;

    private Quaternion rotation;
    public float rotatingVelocityOf=1;

    private void Start()
    {
        g = gameObject;
        
        rotation = g.transform.rotation;

    }


    private void Update()
    {
        var delta = GameManager.instance.delta*rotatingVelocityOf;
        Debug.Log($"x:{delta.x} y:{delta.y}");

        if (delta.x == 0 && delta.y == 0)
        {
            transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,0),Time.deltaTime);
        }
        else
        {
            transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.Euler(delta.y, -delta.x, 0),Time.deltaTime);
        }

    }
    
}
