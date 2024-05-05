using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FeetPlacement : MonoBehaviour
{
    public TwoBoneIKConstraint iKConstraintL;
    public TwoBoneIKConstraint iKConstraintR;
    public Transform footPositionL;
    public LayerMask ground;
    public Transform footPositionR;

    public Transform walkTowardsL;
    public Transform walkTowardsR;
    
    public Transform raycastPelvis;
    
    public GameObject leftPrefabToSpawn;
    public GameObject rightPrefabToSpawn;
    private GameObject currentPrefabToSpawn;    
    public GameObject oldPrefab;
    public float offSet;

    
    private Vector3 direction;
    private Vector3 footPositionLV;
    private Vector3 footPositionRV;
    public float walkSpeed;
    public float distanceFromPelvis;
   
   public bool stepPosnew = false;
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
       
       
        RaycastHit positionPelvis;
        if(Physics.Raycast(raycastPelvis.position, direction, out positionPelvis , Mathf.Infinity,7))
        {
            distanceFromPelvis = Vector3.Distance( positionPelvis.point,footPositionL.position);
            if(distanceFromPelvis >= tresHoldStep)
            {
                if(!stepPosnew)
                {
                    StartCoroutine(InstantiateNextStep());
                    stepPosnew = true;
                }
                
                //Vector3.Lerp(footPositionL,)
            }
        }
    }


        public IEnumerator  InstantiateNextStep()
    {
       
        Vector3 forwardOffset = (lastStep == "R" ? footPositionR.forward : footPositionL.forward) * offSet;
        Vector3 offSetV = oldPrefab.transform.position + forwardOffset;
        currentPrefabToSpawn = lastStep == "R" ? rightPrefabToSpawn : leftPrefabToSpawn;
        GameObject newSpawned = GameObject.Instantiate(currentPrefabToSpawn, offSetV, Quaternion.identity);
        
        TwoBoneIKConstraint currentConstraint = lastStep == "R" ? iKConstraintR:iKConstraintL;
        
        if(oldPrefab != null){
            Destroy(oldPrefab);

        }
        oldPrefab = newSpawned;

        Vector3 currentTargetPos = currentConstraint.data.target.position;
        float lerpTime = 1f;
        float startTime = Time.time;

        

        
        while(Time.time - startTime < lerpTime)
        {
            float t = (Time.time-startTime)/lerpTime;
            currentConstraint.data.target.position = Vector3.Lerp(currentTargetPos, newSpawned.transform.position, t);
            yield return null;

        }   
        currentConstraint.data.target.position = newSpawned.transform.position;
        lastStep = lastStep =="R" ? "L" : "R";
        stepPosnew = false;
    }
}
