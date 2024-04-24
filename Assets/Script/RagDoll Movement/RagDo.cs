using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RagDo : MonoBehaviour
{
    public Transform leftLegPosition;
    public Transform rightLegPosition;
    public Transform leftRayPosition;
    public Transform rightRayPosition;
    public Transform hips;

    public float legStepUp;
    public float DistantL;
    public float DistantR;

    public bool lStepping;
    public bool rStepping;

    private string lastStep;

    public float wantToStepAt;
    public float legSpeed;
    public Vector3 rightFootPosShould;
    public Vector3 leftFootShouldPos;

    public Vector3 shouldReallyBeL;
    public Vector3 shouldReallyBeR;





   
    // Start is called before the first frame update    
    void Start()
    {
        
        shouldReallyBeL = leftLegPosition.position;
        shouldReallyBeR = rightLegPosition.position;
        lastStep ="L";
    }

    // Update is called once per frame
    void Update()
    {
      RaycastHit hitL; 
      if(Physics.Raycast(leftRayPosition.transform.position,Vector3.down,out hitL,5f))
      {
        Debug.Log("" + hitL.collider.name);
        shouldReallyBeL = hitL.point;
      }
      RaycastHit hitR;
      if(Physics.Raycast(rightRayPosition.transform.position,Vector3.down,out hitR,5f))
      {
        Debug.Log("" + hitL.collider.name);
        shouldReallyBeR = hitR.point;
      }

      DistantL =Vector3.Distance(leftFootShouldPos,shouldReallyBeL);
      DistantR =Vector3.Distance(rightFootPosShould,shouldReallyBeR);

      leftLegPosition.position = Vector3.Lerp(leftLegPosition.position,leftFootShouldPos,legSpeed*Time.deltaTime);
      rightLegPosition.position = Vector3.Lerp(rightLegPosition.position,rightFootPosShould,legSpeed*Time.deltaTime);
      if(!lStepping && !rStepping)
      {
        if(lastStep == "R")
        {
            if(DistantL> wantToStepAt)
            {
                StartCoroutine(StepL());
                lastStep = "L";
            }
        }
        else if(lastStep== "L")
        {
            if(DistantR > wantToStepAt)
            {
                StartCoroutine(StepR());
                lastStep = "R";
            }
        }
      }
     //leftLegPosition.transform.eulerAngles = new Vector3(0, -hips.eulerAngles.y, 0);
      //rightLegPosition.transform.eulerAngles = new Vector3(0, -hips.eulerAngles.y, 0);
    }

    public IEnumerator StepL()
    {
        lStepping = true;
        leftFootShouldPos = new Vector3(leftFootShouldPos.x, shouldReallyBeL.y + legStepUp, leftFootShouldPos.z);
        yield return new WaitForSeconds(.3f);
        leftFootShouldPos = shouldReallyBeL;
        yield return new WaitForSeconds(0.2f);
        lastStep = "L";
        lStepping = false;
    }
    public IEnumerator StepR()
    {
        rStepping = true;
        rightFootPosShould = new Vector3(rightFootPosShould.x, shouldReallyBeR.y + legStepUp, rightFootPosShould.z);
        yield return new WaitForSeconds(.3f);
        rightFootPosShould = shouldReallyBeR;
        yield return new WaitForSeconds(0.2f);
        lastStep = "R";
        rStepping = false;
       
    }
}
