using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Density : MonoBehaviour
{
    public Camera main;
	float d;
	bool v;
	
    void Update()
    {
		d = Mathf.PingPong(Time.realtimeSinceStartup, 1);
        RenderSettings.fogDensity = d;

		RenderSettings.fogColor = new Color(d+0.01f,0f,d, d);
		
		
    }
	
	void OnGUI()
	{
		if(Time.realtimeSinceStartup > 1f && main.clearFlags != CameraClearFlags.Depth) 
		{
			v = !v;
			main.clearFlags = CameraClearFlags.Depth;
		}
	}

}
