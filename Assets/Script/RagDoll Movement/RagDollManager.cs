using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollManager : MonoBehaviour
{   
    public ConfigurableJoint [] joint; 
    public GameObject [] bones;
    public GameObject head;
    private Vector3 direction;
    private Rigidbody rb;
    public float speed;
   
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.up;
        rb = head.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        rb.AddForce(direction*speed);
        for (int i = 0; i < joint.Length; i++)
        {
            Vector3 positionToGoTo = bones[i].transform.position;
            Quaternion rotationToGoTo = bones[i].transform.localRotation.normalized;
            
            joint[i].targetPosition = positionToGoTo;
            joint[i].targetRotation = rotationToGoTo;

        }
    }
}
