using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform startPos;
    public Transform endPointForAttack;
    public Transform armTransform;
    public Transform attackTransformHeavy;
    public Transform swordSlamDown;
    public float time;
    public float reach;
    public AnimationCurve reachCurve;
    public float swingDuration;
    public bool swordHeavyAttack, hasRotatedSword;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime / swingDuration;

        if (time >1) time = 1;

        
        if (!swordHeavyAttack)
        {
            SwordToHeaveAttackPos(time);
        }
        else
        {
            HeavyAttack(endPointForAttack,time);
        }
    }
    private void HeavyAttack(Transform endPointPos, float curveValie)
    {
        endPointPos.position = attackTransformHeavy.position;
        armTransform.position = CalculateLocation(curveValie, endPointPos);
        armTransform.rotation = CalculateRotation(curveValie, endPointPos);
        if(!hasRotatedSword)
        {
            armTransform.Rotate(0, 0, 90, Space.Self);
            hasRotatedSword = true;
        }
        
    }
    private void SwordToHeaveAttackPos(float time)
    {
        reach = reachCurve.Evaluate(time);
        armTransform.position = CalculateLocation(reach,endPointForAttack);
        armTransform.rotation = CalculateRotation(reach,endPointForAttack);
        swordHeavyAttack = true;
    }
    private Vector3 CalculateLocation(float curveValue,Transform endPointPos)
    {
        return Vector3.Lerp(startPos.transform.position, endPointPos.position, curveValue);
    }
    private Quaternion CalculateRotation(float curveValue,Transform endPointPos)
    {
        return Quaternion.Slerp(startPos.rotation, endPointPos.rotation, curveValue);
    }
}
