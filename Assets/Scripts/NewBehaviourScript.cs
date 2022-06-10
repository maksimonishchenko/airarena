using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var to = new Vector3(3.14563245211f, 3.145263245211f, 3.14563245231f);
        transform.GetComponent<Rigidbody>().AddTorque(to);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

