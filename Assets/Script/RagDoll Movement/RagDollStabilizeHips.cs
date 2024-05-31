using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class RagDollStabilizeHips : MonoBehaviour
{
    // Stabilizing
    
    public Rigidbody body,spine, head;
    public Transform desiredPosition;
    public float porportionalGain;
    public float intergralGain;
    public float devirativeGain;
    private Vector3 previousError;
    private Vector3 intergral;
   
    
    public GameObject mainObject;
    
    //Leg Placement
    public Rigidbody[] ragdollRigidbodies;
    public Transform leftFoot;
    public Transform rightFoot;
    public Transform leftFootTargetIK;
    public Transform rightFootTargetIK;
    public float lerpSpeed = 5;
    public LayerMask ground;

    //LegSwitch
    private enum Foot {Left,Right}
    private Foot movingFoot = Foot.Left;
    public float stepTimer;
    public float stepDuration = 1;



    // Distance Check Player
    public float treshHold;
   
    public float distantPlayer;
    public Transform player;
    public float time;
    public float speed;
    // Start is called before the first frame update

    void Start()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        stepTimer = stepDuration;
    }

    // Update is called once per frame

    private void Update()
    {
       
        leftFootTargetIK.rotation = transform.rotation;
        rightFootTargetIK.rotation = transform.rotation;
       
    }
    void FixedUpdate()
    {
        
        //Look At

        // Stabilizing using PID formule TO make him always give a aforce back to its desired position.
        Vector3 currentError = desiredPosition.position - body.position;
        intergral += currentError * Time.fixedDeltaTime;
        Vector3 derivative = (currentError - previousError) / Time.fixedDeltaTime;
        previousError = currentError;
        Vector3 force = (porportionalGain * currentError) + (intergralGain * intergral) + (devirativeGain * derivative);
        body.AddForce(force);
       
        
        
    }
    private void LateUpdate()
    {
        stepTimer -= Time.deltaTime;
        Vector3 centerOfMass = CenterOfMass(ragdollRigidbodies);
        if (stepTimer<=0)
        {
            movingFoot = movingFoot == Foot.Left ? Foot.Right : Foot.Left;
            stepTimer = stepDuration;

        }
        if(movingFoot == Foot.Left)
        {
            Vector3 leftFootGroundPos = GroundPos(leftFoot.position);
            Vector3 leftFootTarget = new Vector3(centerOfMass.x - 0.2f, leftFootGroundPos.y, centerOfMass.z);
            leftFootTargetIK.position = Vector3.Lerp(leftFootTargetIK.position, leftFootTarget, Time.deltaTime * lerpSpeed);
        }
        else
        {
            Vector3 rightFoorGroundPos = GroundPos(rightFoot.position);
            Vector3 rightFootTarget = new Vector3(centerOfMass.x + 0.2f, rightFoorGroundPos.y, centerOfMass.z);
            rightFootTargetIK.position = Vector3.Lerp(rightFootTargetIK.position, rightFootTarget, Time.deltaTime * lerpSpeed);
        }
        

        
       

        
        

        
        
        
    }
    private Vector3 CenterOfMass(Rigidbody[] rigidbody)
    {
        Vector3 centerOfMass = Vector3.zero;
        float totalMass = 0;
        foreach (Rigidbody r in rigidbody)
        {
            centerOfMass += r.worldCenterOfMass * r.mass;
            totalMass += r.mass;
        }
        return centerOfMass/ totalMass;
    }

    private Vector3 GroundPos(Vector3 feetPos,float rayLenght = 10)
    {
        RaycastHit hit;
        if(Physics.Raycast(feetPos,Vector3.down,out hit,rayLenght *2,ground))
        {
            
            return hit.point;
        }
        return feetPos;
    }
   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(leftFootTargetIK.position + Vector3.up ,Vector3.down * 2);
        Gizmos.DrawRay(rightFootTargetIK.position + Vector3.up, Vector3.down * 2);



    }
}
