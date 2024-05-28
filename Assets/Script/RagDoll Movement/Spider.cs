using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
   // Pid Controller
    public Rigidbody body;
    public Transform desiredPosition;
    public float porportionalGain;
    public float intergralGain,intergralClamp;
    public float devirativeGain;
    private Vector3 previousError;
    private Vector3 intergral;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        Vector3 currentError = desiredPosition.position - body.position;
        intergral += currentError * Time.fixedDeltaTime;
        //intergral = Vector3.ClampMagnitude(intergral,intergralClamp);
        Vector3 derivative = (currentError - previousError) / Time.fixedDeltaTime;
        previousError = currentError;
        Vector3 force = (porportionalGain * currentError) + (intergralGain * intergral) + (devirativeGain * derivative);
        body.AddForce(force);
    }
   
}
