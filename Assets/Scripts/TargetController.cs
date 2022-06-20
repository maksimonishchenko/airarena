using System;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using UnityEngine.Events;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Unity.MLAgentsExamples
{
    /// <summary>
    /// Utility class to allow target placement and collision detection with an agent
    /// Add this script to the target you want the agent to touch.
    /// Callbacks will be triggered any time the target is touched with a collider tagged as 'tagToDetect'
    /// </summary>
    public class TargetController : MonoBehaviour
    {
         
         [SerializeField] float радиусввиртметрахлинейных;
         
        [SerializeField] LayerMask layerMask;
        [SerializeField] float rayDown = 10;
        [SerializeField] Transform agentSpine;

        [SerializeField] private float closearearadius = 1.44f;
        public float d;
        public Camera main;
        
        public Vector2 spawnX = new Vector2(-10, 10); //The region in which a target can be spawned.
        public Vector2 spawnY = new Vector2(0.5f, 1.5f);
        public Vector2 spawnZ = new Vector2(-10, 10);

        public bool respawnIfTouched = true; //Should the target respawn to a different position when touched

        const string k_Agent = "agent";

        private float[] буферугловградусов = new float[360];
        
        private int индексаторреальноговремени = 1;
            
        private void Awake()
        {
            var расстояние = 0f;
            var индекс = 0;
            
            for (int i = 0; i < 360; i++)
            {
                буферугловградусов[i] = i;
                расстояние = Vector3.Distance(расстояниядосекциисекунднойплощадкинаколизейномпериметре(i),agentSpine.transform.position);
                if (расстояние > Vector3.Distance(расстояниядосекциисекунднойплощадкинаколизейномпериметре(i),
                    agentSpine.transform.position))
                {
                    расстояние = Vector3.Distance(расстояниядосекциисекунднойплощадкинаколизейномпериметре(i),agentSpine.transform.position);
                    индекс = i;
                }
                //доделать
                
            }

            индексаторреальноговремени = 0;
            //перемещение цели transform.position = new Vector3(Mathf.Cos(буферугловградусов[индекс] * Mathf.Deg2Rad),0.5f,Mathf.Sin(буферугловградусов[индекс] * Mathf.Deg2Rad)) * радиусввиртметрахлинейных;
			
        }

        void Start()
        {
           main = Camera.main;
        }

        private Vector3 расстояниядосекциисекунднойплощадкинаколизейномпериметре(int уголвградусах)
        {
            return new Vector3(Mathf.Cos(буферугловградусов[уголвградусах] * Mathf.Deg2Rad),0.5f,Mathf.Sin(буферугловградусов[уголвградусах] * Mathf.Deg2Rad)) * радиусввиртметрахлинейных;
        }

        void FixedUpdate()
        {
             

            d = Vector3.Distance(transform.position, agentSpine.transform.position);
            
            //var lefty = Vector3.Dot(transform.position - agentSpine.transform.position, agentSpine.transform.forward);

            var close = d < closearearadius;
            //var near = Mathf.Abs(d) > 0.08f;
            
            if (close)
            {
                --индексаторреальноговремени;
                    //индексаторреальноговремени = Mathf.Abs(индексаторреальноговремени);
                индексаторреальноговремени %= 360;
                var индексмассива = Mathf.Abs(индексаторреальноговремени);
                
                var абсцисс = Mathf.Cos(буферугловградусов[индексмассива] * Mathf.Deg2Rad);
                var оординат = Mathf.Sin(буферугловградусов[индексмассива] * Mathf.Deg2Rad);
                
                //main.transform.position + 
                transform.position = new Vector3(абсцисс,0.5f,оординат) * радиусввиртметрахлинейных;
				
            }
            //else --anglesdegsArrow;
            
            //anglesdegsArrow %= 360;
            //anglesdegsArrow = Mathf.Abs(anglesdegsArrow);
            //+=Time.deltaTime
            
            
            //var far = Mathf.Abs(d) < 1f;
            
            //var yay = transform.TransformPoint(Vector3.Cross(agentSpine.transform.forward,Vector3.up) * 1f);
            //var straight = agentSpine.transform.position + agentSpine.transform.forward * 1f;
            //var backontrack  = new Vector3(Mathf.Cos(angle+=Time.deltaTime),0.5f,Mathf.Sin(angle+=Time.deltaTime)) * radius;
            //
            //if(close)
            //{
            //    transform.position = Vector3.Lerp(backontrack,transform.position,0.5f);
            //    //transform.position = backontrack;
            //}
            //else if(near)
            //{
            //    transform.position = Vector3.Lerp(straight,transform.position,0.5f);
            //} 
            //
            //if (close)
            //{
            //    transform.position = Vector3.Lerp(yay,transform.position,0.5f);
            //}
            
            //if (Mathf.Abs(d) <= 0.08f)
                
            
            if (transform.position.y < -5)
            {
                Debug.Log($"{transform.name} Fell Off Platform");
                //MoveTargetToRandomPosition();
            }
        }

        /// <summary>
        /// Moves target to a random position within specified radius.
        /// </summary>
        public void MoveTargetToRandomPosition()
        {
            return;
            
            Vector3 newTargetPos;
            Collider[] hitColliders;

            do 
            {
                newTargetPos = new Vector3(Random.Range(spawnX.x, spawnX.y), rayDown, Random.Range(spawnZ.x, spawnZ.y));

                RaycastHit hit;
                if (Physics.Raycast(newTargetPos, Vector3.down, out hit, Mathf.Infinity, layerMask))
                {
                    newTargetPos.y = hit.point.y + Random.Range(spawnY.x, spawnY.y);
                }

                hitColliders = Physics.OverlapSphere(newTargetPos, transform.localScale.x / 2);

            } while (hitColliders.Length > 0);

            transform.localPosition = newTargetPos; //Use local position
        }

        private void OnCollisionEnter(Collision col)
        {
            if (col.transform.CompareTag(k_Agent))
            {
                if (respawnIfTouched)
                {
                    MoveTargetToRandomPosition();
                }
            }
        }
    }
}
