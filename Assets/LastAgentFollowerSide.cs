using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LastAgentFollowerSide : MonoBehaviour
{
    public Vector3 shift;
    public float speed;
    public List<GameObject> agents;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agents.RemoveAll(a=>a == null);

        var lastAgentZ = agents.Min(g=>g.transform.Find("RobotKyle/OrientCube").position.z);
        var lastAgentX = agents.Min(g=>g.transform.Find("RobotKyle/OrientCube").position.x);
        var lastAgentY = agents.Min(g=>g.transform.Find("RobotKyle/OrientCube").position.x);

        //Debug.Log(lastAgentZ);
        transform.position = Vector3.Lerp(transform.position, new Vector3(lastAgentX + shift.x ,lastAgentY + shift.y,lastAgentZ + shift.z),Time.deltaTime * speed);
    }
}
