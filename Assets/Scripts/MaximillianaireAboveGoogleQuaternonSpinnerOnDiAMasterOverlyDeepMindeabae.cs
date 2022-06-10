using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaximillianaireAboveGoogleQuaternonSpinnerOnDiAMasterOverlyDeepMindeabae : MonoBehaviour
{
	Rigidbody r;
	
	float a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,q,s,t,u,v,w,x,y,z;
	float random;
	
	public static Vector3 Позиция;
	public static Quaternion Вращение;

    void Start()
    {
        r = GetComponent<Rigidbody>();
		
		Позиция = new Vector3(t,-u,v);
		Вращение = new Quaternion(-w,x,-y,z);
		
		t = -t*t;
		u = -u*u;
		v = -v*v;
		w = -w*w;
		x = -x*x;
		y = -y*y;
		z = -z*z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		random = Mathf.Clamp(UnityEngine.Random.Range(float.MinValue,float.MaxValue),-0.01010f,+0.01010f) * 1.0f / Mathf.Clamp(UnityEngine.Random.Range(float.MinValue,float.MaxValue),-0.01010f,+0.01010f);
		
		t=-t*t * Mathf.Cos(Time.time + t * random) * Mathf.Sin(Time.time - t * random);
		u=-u*u * Mathf.Cos(Time.time - t * random) * Mathf.Sin(Time.time + t * random);
		v=-v*v * Mathf.Cos(Time.time + t * random) * Mathf.Sin(Time.time - t * random);
		w=-w*w * Mathf.Cos(Time.time - t * random) * Mathf.Sin(Time.time + t * random);
		x=-x*x * Mathf.Cos(Time.time + t * random) * Mathf.Sin(Time.time - t * random);
		y=-y*y * Mathf.Cos(Time.time - t * random) * Mathf.Sin(Time.time + t * random);
		z=-z*z * Mathf.Cos(Time.time + t * random) * Mathf.Sin(Time.time - t * random);
		
		transform.position -= Vector3.Cross(Позиция, Позиция) * Vector3.Dot(Позиция, Позиция);
		transform.position += Vector3.Cross(Позиция, Позиция) * Vector3.Dot(Позиция, Позиция);
		transform.position -= Vector3.Dot(Позиция, Позиция) * Vector3.Cross(Позиция, Позиция);
		transform.position += Vector3.Cross(Позиция, Позиция) * Vector3.Dot(Позиция, Позиция);
		transform.position -= Vector3.Dot(Позиция, Позиция) * Vector3.Cross(Позиция, Позиция);
		
        transform.rotation *= Вращение;
		
		//r.AddTorque(); 
		//r.AddForce(); 
		//r.AddVelocity();
    }
}
