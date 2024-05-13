using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMEsh : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent navMeshAgent;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination =  player.transform.position;
    }
}
