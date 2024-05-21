using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMEsh : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform desiredPosition;
    public Transform body;
    public Vector3 previousError;
    public float porportionalGain, intergralGain, derivativeGain;
    public Vector3 intergral;
    public Rigidbody bodyRB;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentError = desiredPosition.position - body.position;
        intergral += currentError * Time.deltaTime;
        Vector3 derivative = (currentError - previousError) / Time.deltaTime;
        previousError = currentError;
        Vector3 force = (porportionalGain * currentError) + (intergralGain * intergral) + (derivativeGain * derivative);
        bodyRB.AddForce(force);
       // head.AddForce(Vector3.up * streghtUp);
    }
}
