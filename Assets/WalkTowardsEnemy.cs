using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkTowardsEnemy : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
  
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
        
        
        

    }
}
