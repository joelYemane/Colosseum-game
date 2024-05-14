using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RagDollManager : MonoBehaviour
{   
    public ConfigurableJoint [] joint; 
    public GameObject [] bones;
    public GameObject head, spine, groundPos;
    private Vector3 direction;
    public Rigidbody[] allBodys; 
    private Rigidbody rb;
    public float speed;
    public float tresholdCenter;
    public float tresholdFellOver;
    public float distanceBody;

    public Transform rayCastFeetPosR, rayCastFeetPosL;
    public bool isGrounded;
    public Vector3 centerOfMass;
    public LayerMask ground;
    public Vector3 originalSPot;
    public float getUpAmount;

    // Start is called before the first frame update
    void Start()
    {
        allBodys = GetComponentsInChildren<Rigidbody>();
        
        direction = Vector3.up;
        rb = head.GetComponent<Rigidbody>();

        for (int i = 0; i < allBodys.Length; i++)
        {
            Vector3 centerOfMass = allBodys[i].centerOfMass;
            originalSPot = centerOfMass;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Vector3 headPos = new Vector3(head.transform.position.x,head.transform.position.y,head.transform.position.z);
       
       
        distanceBody = Vector3.Distance(headPos, centerOfMass);
        
        if(headPos.y < tresholdFellOver)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded= true;
        }
        
        if(isGrounded)
        {
            for (int i = 0; i < joint.Length; i++)
            {
                Vector3 positionToGoTo = -bones[i].transform.position;
                Quaternion rotationToGoTo = bones[i].transform.localRotation.normalized;

                joint[i].targetPosition = positionToGoTo;
                joint[i].targetRotation = rotationToGoTo;

            }
        }
        
        for (int i = 0; i < allBodys.Length; i++)
        {
           centerOfMass = allBodys[i].centerOfMass;
           
        }
        if(distanceBody > tresholdCenter)
        {
            Vector3 desiredSpot = DesiredCenterOfMass();
            Vector3 nowSpot = CurrentCenterOfMass();
            Vector3 forceDirection = (nowSpot - desiredSpot).normalized;
            head.GetComponent<Rigidbody>().AddForce(-forceDirection* speed);
        }
       
    }
    private Vector3 CurrentCenterOfMass()
    {
        for(int i = 0;i <allBodys.Length; i++)
        {
            centerOfMass = allBodys[i].centerOfMass;
            centerOfMass /= allBodys.Length;
        }
        return centerOfMass;
    }
    private Vector3 DesiredCenterOfMass()
    {
        return originalSPot ;
    }
    //public void GetUpSleepy()
    //{

    //    for (int i = 0; i < joint.Length; i++)
    //    {
    //        getUpAmount = joint[i].angularXDrive.positionSpring;
    //        getUpAmount = joint[i].angularYZDrive.positionSpring;
    //        Debug.Log(joint[i].angularYZDrive);

    //    }
    //}
}
