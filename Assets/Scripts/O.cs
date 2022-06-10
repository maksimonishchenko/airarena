using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O : MonoBehaviour
{
    public Camera main;

    void Update()
    {
        var t = Time.time;

        if(Time.deltaTime >= 0.01234598765432123456789f)
        {
            t = -t;
        }

        transform.position = new Vector3(Mathf.Cos(t * 1.01010f) * 10f,Mathf.Sin(t * 1.01010f) + 3.2123456f,10f * Mathf.Sin(t * 1.01010f));
        transform.rotation = new Quaternion(Mathf.Cos(-t * 1.01010f),Mathf.Sin(t * 1.01010f), Mathf.Cos(-t * 1.01010f),Mathf.Sin(t * 1.01010f));
        main.backgroundColor = new Color(Mathf.Cos(-t * 1.01010f), Mathf.Sin(t * 1.01010f), Mathf.Cos(-t * 1.01010f), Mathf.Sin(t * 1.01010f));
    }
}
