using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabilizing : MonoBehaviour
{
    public Transform hips , leftShoulder, rightShoulder ;
    public Transform targetHips, targetLeft, targetRight ;

    public float moveForce;
    public float stabilityForce;
    public float devirativeForce;

    private Vector3 intergral;
    private Vector3 lastError;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        ApplyForces(hips, targetHips);
        ApplyForces(rightShoulder, targetRight);
        ApplyForces (leftShoulder, targetLeft);
    }

    private void ApplyForces(Transform bodyPart, Transform targetPart)
    {
        Rigidbody rb = bodyPart.GetComponent<Rigidbody>();
        if (rb != null )
        {
            Vector3 error = targetPart.position - bodyPart.position;
            intergral += error*Time.fixedDeltaTime;
            Vector3 derivaative =(error - lastError)/Time.fixedDeltaTime;
            lastError = error;

            Vector3 pidForce = moveForce * error + stabilityForce *intergral + devirativeForce *derivaative;

            rb.AddForce(pidForce);
        }
    }
}
