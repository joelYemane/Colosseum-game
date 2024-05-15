using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceHeadPull : MonoBehaviour
{
    public float speed;
    public Rigidbody head;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        head = GetComponent<Rigidbody>();
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        head.AddForce(Vector3.up *speed * Time.deltaTime,ForceMode.VelocityChange);
    }
}
