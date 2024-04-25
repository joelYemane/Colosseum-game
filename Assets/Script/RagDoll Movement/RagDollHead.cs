using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollHead : MonoBehaviour
{
    private Rigidbody rbHead;
    private Vector3 direction;
    public float pullUpForce;
    // Start is called before the first frame update
    void Start()
    {
        rbHead = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = new Vector3(0,1,0);
        rbHead.AddForce(direction*pullUpForce,ForceMode.Force);
    }
}
