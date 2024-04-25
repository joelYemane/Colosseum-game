using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollFeet : MonoBehaviour
{
    public GameObject leftFeetObect;
    public GameObject rightFeetObect;
    private Rigidbody leftFeet;
    private Rigidbody rightFeet;
    private Vector3 direction;

    public float downForceLeft;
    public float downForceRight;
    // Start is called before the first frame update
    void Start()
    {
        leftFeet = leftFeetObect.GetComponent<Rigidbody>();
        rightFeet= rightFeetObect.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = new Vector3(0,-1,0);
        leftFeet.AddForce(direction*downForceLeft,ForceMode.Force);
        rightFeet.AddForce(direction*downForceRight,ForceMode.Force);
    }
}
