using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabilizing : MonoBehaviour
{
    public Transform hips , leftShoulder, rightShoulder ;
    public Transform targetHips, targetLeft, targetRight ;
    public GameObject targetL;
    public GameObject targetR;
    public Rigidbody enemyMainObject;
    public Transform raycastPosl, raycastPosR;
    public float moveForce, feetSpeed;
    public float stabilityForce, journey;
    public float devirativeForce;
    public Vector3 startPosition, endPos;
    public int index;
    private Vector3 intergral;
    private Vector3 lastError;
    public bool hasFiredRaycast;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyForces(hips, targetHips);
        ForceForwards();
        switch (index)
        {
            case 0:
                StartCoroutine(MoveFoot(targetL, raycastPosl.transform.position));


                break;
            case 1:
                StartCoroutine(MoveFoot(targetR, raycastPosR.transform.position));


                break;

        }
        
    }

    public IEnumerator MoveFoot(GameObject foot, Vector3 rayCastPos)
    {
        journey = 0;

        if (Physics.Raycast(rayCastPos, Vector3.down, out RaycastHit hit, 8, ground))
        {
            hasFiredRaycast = true;
            startPosition = foot.transform.position;
            endPos = hit.point;

        }

        while (journey <= 1 && hasFiredRaycast)
        {
            journey += Time.fixedDeltaTime * feetSpeed;
            foot.transform.position = Vector3.Lerp(startPosition, endPos, journey);
            hasFiredRaycast = false;
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
    // hips
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
    public void ForceForwards()
    {
       
        //enemyMainObject.velocity = new Vector3(0, 0, 0.5f);
    }
}
