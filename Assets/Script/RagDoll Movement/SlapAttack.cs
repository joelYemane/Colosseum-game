using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapAttack : MonoBehaviour
{
   
    public float speed;
    public GameObject aI;
    public Rigidbody arm;
        // Start is called before the first frame update
    public 
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            Vector3 directionToAttack = (arm.transform.position - aI.transform.position).normalized;
            Vector3 torque = Vector3.Cross(transform.forward, directionToAttack) * speed;
            arm.AddTorque(torque, ForceMode.VelocityChange);
        }
    }
}
