using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class FeetPlacement : MonoBehaviour
{
    // attack
    public float force;
    public GameObject sword;
    public Rigidbody arm;
    public float attackCooldown;
    public GameObject IKTargetLeft;
    public GameObject IKTargetRight;
    public Transform[] attackPoints;
    public float attackSpeed;
    //Ragdoll Walk Force

    public Transform player;
    public float stoppingDistance;
    public float multiplier;
    //Leg Placement
    public Rigidbody[] ragdollRigidbodies;
    public Transform leftFoot;
    public Transform rightFoot;
    public Transform leftFootTargetIK;
    public Transform rightFootTargetIK;
    public float lerpSpeed = 5;
    public LayerMask ground;
    public Transform raycastL;
    public Transform raycastR;
    public float distance;
    
    //LegSwitch
    private enum Foot { Left, Right }
    private Foot movingFoot = Foot.Left;
    public float stepTimer;
    public float stepDuration = 1;
    // Start is called before the first frame update
    void Start()
    {
        //agent.destination = player.position;
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        stepTimer = stepDuration;
    }

    // Update is called once per frame
    void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0)
        {
            Debug.Log("Slam");
           
            attackCooldown = 5;
        }

        transform.LookAt(player);
        Debug.Log(movingFoot);
        
            //Vector3 centerOfMass = CenterOfMass(ragdollRigidbodies);
            if (stepTimer <= 0)
            {
                movingFoot = movingFoot == Foot.Left ? Foot.Right : Foot.Left;
                stepTimer = stepDuration;

            }
            if (movingFoot == Foot.Left)
            {
                Vector3 leftFootGroundPos = GroundPos(raycastL.position);
                Vector3 leftFootTarget = new Vector3(leftFootGroundPos.x + 0.4f, 0, leftFootGroundPos.z);
                leftFootTargetIK.localPosition = Vector3.Lerp(leftFootTargetIK.position, leftFootTarget, Time.deltaTime * lerpSpeed);
            }
            else
            {
                Vector3 rightFoorGroundPos = GroundPos(raycastR.position);
                Vector3 rightFootTarget = new Vector3(rightFoorGroundPos.x - 0.4f, 0, rightFoorGroundPos.z);
                rightFootTargetIK.localPosition = Vector3.Lerp(rightFootTargetIK.position, rightFootTarget, Time.deltaTime * lerpSpeed);
            }

       
        stepTimer -= Time.deltaTime;


    }
    public void FixedUpdate()
    {
        MoveTowardsPlayer();
        


    }

    private void LateUpdate()
    {
        
    }
    
    public void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position);
        distance = Vector3.Distance(transform.position, player.position);
        if(distance >= stoppingDistance)
        {
            foreach(Rigidbody rb in ragdollRigidbodies)
            {
                rb.AddForce(direction* multiplier);
            }
        }
        else
        {
            foreach (Rigidbody rb in ragdollRigidbodies)
            {
                Debug.Log("Hit");
                rb.velocity = Vector3.zero;
            }
        }
       
    }

    private Vector3 GroundPos(Vector3 feetPos, float rayLenght = 10)
    {
        RaycastHit hit;
        if (Physics.Raycast(feetPos, Vector3.down, out hit, rayLenght * 2, ground))
        {

            return hit.point;
        }
        return feetPos;
    }
}
