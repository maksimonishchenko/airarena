using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace JKress.AITrainer
{
    public class PrefabSpawner : MonoBehaviour
    {
        [SerializeField] LastAgentFollowerSide[] cameraTargetGetter;
        [SerializeField] GameObject[] basePrefab;

        [SerializeField] int xCount = 5;
        [SerializeField] int zCount = 5;

        [SerializeField] float offsetX = 20f;
        [SerializeField] float offsetZ = 20f;

        GameObject scenePrefab;

        public void Spawn()
        {
            scenePrefab = GameObject.FindWithTag("agentPrefab");
            if (scenePrefab != null) Destroy(scenePrefab); //If prefab is in the scene, remove it

            float behaviorOffset = 0;
            List<GameObject>agents = new List<GameObject>();
            List<Transform> targets = new List<Transform>();
            for (int k = 0; k < basePrefab.Length; k++)
            {
                //Spawn prefabs along x and z from basePrefab 
                for (int i = 0; i < xCount; i++)
                {
                    for (int j = 0; j < zCount; j++)
                    {
                        var agent = Instantiate(basePrefab[k], new Vector3( /*i **/ offsetX + behaviorOffset, 0, j * offsetZ), Quaternion.identity); 
                        agents.Add(agent);
                        //
                    }
                }
            }
            if(cameraTargetGetter !=  null)
            {
                for(int i = 0; i < cameraTargetGetter.Length; i++)
                {
                    if (cameraTargetGetter[i].agents == null)
                    {
                        cameraTargetGetter[i].agents = new List<GameObject>();
                        cameraTargetGetter[i].AgentsChanged();
                    }
                        

                    cameraTargetGetter[i].agents.AddRange(agents);
                    cameraTargetGetter[i].AgentsChanged();
                }
            }
        }
    }
}
