using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetPlacement : MonoBehaviour
{
    public Transform footPositionL;
    public Transform footPositionR;

    
    public Vector3 rayCastHitPositionR;
    public Vector3 rayCastHitPositionL;

    public Transform raycastPositionL;
    public Transform raycastPositionR;
    private Vector3 direction;
    private Vector3 footPositionLV;
    private Vector3 footPositionRV;
    public float walkSpeed;
    public float distanceBetweenStepL;
    public float distanceBetweenStepR;
    public float tresHoldStep;
    public bool leftFootStep;
    public bool rightFootStep;
    public string lastStep;

    // Start is called before the first frame update
    void Start()
    {
        lastStep = "R";
        direction = new Vector3(0, -1,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         RaycastHit hitRayCastR;
         RaycastHit hitRaycastL;
        footPositionLV = new Vector3(raycastPositionL.transform.position.x, raycastPositionL.transform.position.y, raycastPositionL.transform.position.z);
        footPositionRV = new Vector3(raycastPositionR.transform.position.x, raycastPositionR.transform.position.y, raycastPositionR.transform.position.z);
        if (Physics.Raycast(raycastPositionL.position, direction, out hitRaycastL, Mathf.Infinity))
        {
            rayCastHitPositionL = hitRaycastL.point;
        }
        if (Physics.Raycast(raycastPositionR.position, direction, out hitRayCastR, Mathf.Infinity))
        {
            rayCastHitPositionR = hitRayCastR.point;
        }
        distanceBetweenStepL = Vector3.Distance(raycastPositionL.transform.position, footPositionL.position);
        distanceBetweenStepR = Vector3.Distance(rayCastHitPositionR, footPositionR.position);
        
        RaycastHit hitR;
        RaycastHit hitL;
        if(distanceBetweenStepL > tresHoldStep || distanceBetweenStepR > tresHoldStep)
        {
            if(!leftFootStep && !rightFootStep)
            {
            
                if(lastStep == "L")
                {
                    rightFootStep = true;
                    if(Physics.Raycast(footPositionRV, direction,out hitR,Mathf.Infinity))
                    {   
                    
                        footPositionR.transform.position = hitR.point;

                    }
                    rightFootStep = false;
                    lastStep ="R";
                }
                else if(lastStep == "R")
                {
                    leftFootStep = true;
                    if(Physics.Raycast(footPositionLV, direction,out hitL,Mathf.Infinity))
                    {   
                    
                        footPositionL.transform.position = hitL.point;
                    
                    }
                    leftFootStep = false;
                  
                    lastStep = "L";
                }
                
            }

        }
        

        
    }
                

private void OnDrawGizmos()
{
    // Draw raycasts
    Gizmos.color = Color.red;
    Gizmos.DrawRay(raycastPositionL.position, direction);
    Gizmos.DrawRay(raycastPositionR.position, direction);

    // Draw foot positions
    Gizmos.color = Color.green;
    Gizmos.DrawSphere(footPositionL.position, 0.1f);
    Gizmos.DrawSphere(footPositionR.position, 0.1f);
}
    public void Walk()
    {
        
        leftFootStep = false;
    }
}
