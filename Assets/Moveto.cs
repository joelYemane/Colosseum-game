using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Moveto : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public Vector3 sword;
    public Transform attachPoint;
    public GameObject realSword;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
        distance = Vector3.Distance(transform.position, agent.destination);
        sword = new Vector3(attachPoint.position.x, attachPoint.position.y, attachPoint.position.z);
        realSword.transform.position = sword;
        gameObject.GetComponent<Animator>().SetFloat("Distance", distance);
        gameObject.GetComponent<Animator>().SetBool("HasAttacked", false);
        if (distance < 1)
        {
            agent.destination = transform.position;
            gameObject.GetComponent<Animator>().SetBool("attacking", true);
            
        }
        else
        {
            agent.destination = player.transform.position;
            gameObject.GetComponent<Animator>().SetBool("attacking", false);
            gameObject.GetComponent<Animator>().SetBool("HasAttacked", true);
        }
    }

    
}
