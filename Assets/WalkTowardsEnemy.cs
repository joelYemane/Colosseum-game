using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTowardsEnemy : MonoBehaviour
{
    public Transform player;
    private Vector3 direction;
    public Rigidbody enemy;
    public float speed;
    public float turnSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - player.position;
        //transform.LookAt(direction);
        enemy.AddForce(-direction*speed * Time.deltaTime);
        

    }
}
