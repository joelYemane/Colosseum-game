using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RagDollManager : MonoBehaviour
{
    //public string lastStep;
    public GameObject targetL, targetR;
    public GameObject head, spine, groundPos;
    //private Vector3 direction;
    public Rigidbody[] allBodys; 
    //private Rigidbody rb;
    public float speed;
    public float tresholdCenter;
    public float tresholdFellOver;
    public float distanceBody;
    public GameObject betweenFeet;
    public Transform rayCastFeetPosR, rayCastFeetPosL;
    public bool isGrounded;
    public Vector3 centerOfMass;
    public LayerMask ground;
    public Vector3 originalSPot,feetGoToSpot;
    public float distanceStepNeeded;
    public Transform distanceStep;
    public int index;
    public bool hasSteppedL, hasSteppedR;
    public GameObject feetL, feetR;
    public float feetSpeed, forceSpeed, journey;
    private Rigidbody enemyMainObject;
    private Vector3 startPosition, endPos;
    public bool hasFiredRayCast;
    public GameObject targetPlayer;
    public float distanceForWalk;
    // Start is called before the first frame update
    void Start()
    {
        allBodys = GetComponentsInChildren<Rigidbody>();
        enemyMainObject = GetComponent<Rigidbody>();

        
        for (int i = 0; i < allBodys.Length; i++)
        {
            Vector3 centerOfMass = allBodys[i].centerOfMass;
            originalSPot = centerOfMass;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        distanceForWalk = Vector3.Distance(transform.position, targetPlayer.transform.position);
        

        Vector3 headPos = new Vector3(head.transform.position.x,head.transform.position.y,head.transform.position.z);
        distanceStepNeeded = Vector3.Distance(distanceStep.transform.position, betweenFeet.transform.position);


        Vector3 betweenFoot = (feetL.transform.position + feetR.transform.position) / 2;
        distanceBody = Vector3.Distance(headPos, centerOfMass);

        betweenFeet.transform.position = betweenFoot;
        if(headPos.y < tresholdFellOver)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded= true;
        }
        if(distanceForWalk >3)
        {
            if (isGrounded)
            {
                // ForceForwards();
                switch (index)
                {
                    case 0:
                        StartCoroutine(MoveFoot(targetL, rayCastFeetPosL.transform.position));


                        break;
                    case 1:
                        StartCoroutine(MoveFoot(targetR, rayCastFeetPosR.transform.position));


                        break;

                }




            }
        }
       

        for (int i = 0; i < allBodys.Length; i++)
        {
            centerOfMass = allBodys[i].centerOfMass;

        }
        if (distanceBody > tresholdCenter)
        {
            Vector3 desiredSpot = DesiredCenterOfMass();
            Vector3 nowSpot = CurrentCenterOfMass();
            Vector3 forceDirection = (nowSpot - desiredSpot).normalized;
            head.GetComponent<Rigidbody>().AddForce(-forceDirection * speed);
        }


    }
    private Vector3 CurrentCenterOfMass()
    {
        for(int i = 0;i <allBodys.Length; i++)
        {
            centerOfMass = allBodys[i].centerOfMass;
            
        }

        return centerOfMass;
    }
    private Vector3 DesiredCenterOfMass()
    {
        return originalSPot;
    }
    
    public IEnumerator MoveFoot( GameObject foot, Vector3 rayCastPos)
    {
        journey = 0;

        if (Physics.Raycast(rayCastPos, Vector3.down, out RaycastHit hit, 4, ground))
        {
            hasFiredRayCast = true;
            startPosition = foot.transform.position;
            endPos = hit.point;

        }

        while (journey <= 1 && hasFiredRayCast)
        {
            journey += Time.deltaTime * feetSpeed;
            foot.transform.position = Vector3.Lerp(startPosition, endPos, journey);
            hasFiredRayCast = false;
            yield return null;
        }

        if (index == 0)
        {
            yield return new WaitForSeconds(1f);
            index = 1;
        }
        else if (index == 1)
        {
            yield return new WaitForSeconds(1f);
            index = 0;
        }

       
    }
    
}
