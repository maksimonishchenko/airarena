using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JKress.AITrainer;
using TMPro;
using UnityEngine.SceneManagement;

public class LastAgentFollowerSide : MonoBehaviour
{
    
    public List<GameObject> agents;
    
    [SerializeField] private TextMeshProUGUI _text;//20.54 0.28
    [SerializeField] private Vector3 _shift;//20.54 0.28
    [SerializeField] private Transform _laserQuad;
    [SerializeField] private AudioClip[] _soundFXs;
    [SerializeField] private GameObject[] _visualFXs;

    private Vector3 _initialPos;
    private float _toTargetClosestDistance = 4f; // height from ground surface
    private float _camAdvantage = 10f;
    private float _robotHeightAvg = 0.45f;
    private float _m_MaxDistance = 40f;
    private float speed  = 0.85f;
    private List<Collider[]> _colliders;
    private List<Transform> _targets;
    private RaycastHit _m_Hit;
    private bool _started;
    private float _finaleShowTime = 5f;
    private bool _finished = false;
    private float _timeFromStart;
    private float _timeDisplayStart = 5f;
    
    void Awake()
    {
        _initialPos = transform.position;
        
        if (_laserQuad == null) Debug.LogError("Assign Laser Quad to Detect Collisions! : LastAgentFollowerSide.cs");
        if (_soundFXs == null) Debug.LogError("Assign _soundFXs : LastAgentFollowerSide.cs");
        if (_visualFXs == null) Debug.LogError("Assign _visualFXs : LastAgentFollowerSide.cs");
        
        var lastAgent = CalculateLastAgentPos();
        var final = GetFinalPos(lastAgent, _shift);
        
        transform.position = final + Vector3.back * _camAdvantage;
        Debug.Log("position camera agents chagnes " + transform.position);
    }
    
    public void AgentsChanged()
    {
        _colliders = agents.Select(a => a.GetComponentsInChildren<Collider>()).ToList();
        _targets = agents.Select(a => a.transform.Find("RobotKyle").GetComponent<WalkerAgent>().TargetT).ToList();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        agents.RemoveAll(a=>a == null);
        AgentsChanged();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_started == false )
        {
            if(_laserQuad.gameObject.activeSelf)
                _laserQuad.gameObject.SetActive(false);
            
            _text.text = "Tap to start!";
            if (Input.GetMouseButtonDown(0))
            {
                var spawners = GameObject.FindObjectsOfType<PrefabSpawner>();
                foreach (var spawner in spawners)
                {
                    spawner.Spawn();    
                }

                _started = true;
                _text.text = "Start!";   
                _laserQuad.gameObject.SetActive(true);
            }
        }

        if (_started == false)
            return;
        
        if (_timeFromStart < _timeDisplayStart)
        {
            _timeFromStart += Time.deltaTime;
            if (_timeFromStart > _timeDisplayStart)
            {
                _text.text = string.Empty;
            }
        }
        
        agents.RemoveAll(a => a == null);
        
        if (agents.Count == 0 && _started)
        {
            if (_finished == false)
            {
                Debug.Log(string.Empty);
                Invoke("Restart", _finaleShowTime);
                _finished = true;
                _text.text = string.Empty;    
            }
            else
            {
                return;
            }
        }
        
        var lastAgent = CalculateLastAgentPos();
        var final = GetFinalPos(lastAgent, _shift);
        var current = GetCurrentPos(final);

        transform.position = current;

        var size = CalculateCubeSize();
        var m_HitDetect = Physics.BoxCast(transform.position + _shift.y * 0.5f * Vector3.forward, size * 0.5f,
            transform.forward, out _m_Hit, Quaternion.identity, _m_MaxDistance, 1 << 0);
        if (m_HitDetect)
        {
            //Output the name of the Collider your Box hit
            if (_colliders.Any(c => c.Any(col => col == _m_Hit.collider)))
            {
                Debug.Log($"Agent {_m_Hit.collider.transform.root.name} hitted  by a laser at point " + _m_Hit.point +
                          "_m_Hit transform bane " + _m_Hit.transform.root.name);
                Debug.Log("Destroying " + _m_Hit.transform.root.gameObject);
                
                var agent = _m_Hit.transform.root.gameObject;
                if (agent != null)
                {
                    var walkerAgent = agent.transform.Find("RobotKyle").GetComponent<WalkerAgent>();
                    walkerAgent.enabled = false;
                    if (_soundFXs != null && _soundFXs.Length > 0)
                    {
                        var rndSFX = _soundFXs[Random.Range(0, _soundFXs.Length)];
                        var source = agent.AddComponent<AudioSource>();
                        source.clip = rndSFX;
                        source.PlayOneShot(source.clip);
                    
                        var rndVFX = _visualFXs[Random.Range(0, _visualFXs.Length)];
                        var visual = Instantiate(rndVFX,_m_Hit.point,Quaternion.FromToRotation(_m_Hit.normal,_m_Hit.normal));
                    }

                    agents.Remove(agent);
                    AgentsChanged();
                }
                else
                {
                    Debug.LogError("agent null! : LastAgentFollowerSlide");
                }
               
            }

        }

        
        var idAgent = 0;
        if (agents.Any(a =>
        {
            bool finished = (Vector3.Distance(a.transform.position, _targets[agents.IndexOf(a)].position) <
                             _toTargetClosestDistance);
            if(finished){
                idAgent = agents.IndexOf(a);
            }
            
            return finished;
        }))
        {
            Debug.Log(idAgent + "finished first place ");
            Invoke("Restart", _finaleShowTime);
            _finished = true;
            _text.text = idAgent + "finished first";
        }
    }
    
    Vector3 GetCurrentPos(Vector3 finalPos)
    {
        return Vector3.Lerp(transform.position, finalPos,Time.deltaTime * speed);
    }
    
    Vector3 GetFinalPos(Vector3 lastAgentPos, Vector3 shift)
    {
        return lastAgentPos + shift;
    }
        
    Vector3 CalculateLastAgentPos()
    {
        if (agents != null && agents.Count > 0)
        {
            var x = agents.Min(g=>g.transform.Find("RobotKyle/OrientCube").position.x);
            var y = agents.Min(g=>g.transform.Find("RobotKyle/OrientCube").position.y);
            var z = agents.Min(g=>g.transform.Find("RobotKyle/OrientCube").position.z);
            return new Vector3(x, y, z);
        }

        return _initialPos;
    }

    private Vector3 CalculateCubeSize()
    {
        return new Vector3(_laserQuad.transform.localScale.z,_robotHeightAvg,_laserQuad.transform.localScale.y);
        
    }
    
    private Vector3 CalculateCubeCastCenterPos()
    {
        return transform.position + _shift.y * 0.5f * Vector3.forward;

    }
    
    private void OnDrawGizmos()
    {
        //var center = CalculateCubeCastCenterPos();
        //var size = CalculateCubeSize();
        //Gizmos.DrawCube(center, size);
        //Gizmos.DrawLine(center,center + transform.forward * _m_MaxDistance);
        //Gizmos.DrawCube(center +  transform.forward * _m_MaxDistance, size);
        //
        //if (_colliders.Any(c => c.Any(col => col == _m_Hit.collider)))
        //{
        //    Gizmos.DrawSphere(_m_Hit.point,0.1f);
        //}
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
