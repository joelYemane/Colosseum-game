using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SelfBalance : MonoBehaviour
{
    public Rigidbody torsoRB;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float maxTorgua = 100f;
    // Start is called before the first frame update
   void FixedUpdate ()
   {
        bool isGrounded = Physics.Raycast(groundCheck.position,Vector3.down,0.1f,groundLayer);
        if(isGrounded)
        {
            Vector3 currentTorque = CalculateBalance();
            torsoRB.AddTorque(currentTorque);
        }
   }
   private Vector3 CalculateBalance()
   {
    Quaternion upRightRotation = Quaternion.identity;
    Quaternion currentRotation =torsoRB.rotation;
    Quaternion rotationDiffrence = Quaternion.Inverse(upRightRotation) * currentRotation;

    Vector3 angularVelocity = rotationDiffrence.eulerAngles;
    Vector3 torque = -angularVelocity *maxTorgua;
    return torque;
   }
}
