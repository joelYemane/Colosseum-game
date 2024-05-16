using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class RagDoll : MonoBehaviour
{
    public Rigidbody[] rigidbodies;
    public float moveForce = 10f;
    public Transform targetPosition;
    public Transform centerOfMass;
    public float stabilityForce;
    public Transform leftHandTarget;
    public Transform rightHandTarget;
    public Transform leftFootTarget;
    public Transform rightFootTarget;
    public TwoBoneIKConstraint leftHandIK;
    public TwoBoneIKConstraint rightHandIK;
    public TwoBoneIKConstraint leftFootIK;
    public TwoBoneIKConstraint rightFootIK;
    private Rigidbody centralRigidbody;

    private Vector3 integral;
    private Vector3 lastError;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.mass = 1;
            rb.drag = 1;
            rb.angularDrag = 1;

            ConfigurableJoint joint = rb.GetComponent<ConfigurableJoint>();
            Collider collider = rb.GetComponent<Collider>();
            if(collider!= null)
            {
                collider.material = new PhysicMaterial
                {
                    dynamicFriction = 0.6f,
                    staticFriction = 0.6f,
                    frictionCombine = PhysicMaterialCombine.Multiply,

                };
            }
            if(joint != null)
            {
                SetupJoints(joint);
            }
        }
        centralRigidbody = centerOfMass.GetComponent<Rigidbody>();
        IgnoreRagdollCollisions();
        StartCoroutine(startRagDoll());
    }
    private IEnumerator startRagDoll()
    {
        yield return new WaitForEndOfFrame();
        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.useGravity = true;
            Debug.Log("There will be Gravity");
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        leftHandTarget.position = CalculateLeftHandPos();
        rightHandTarget.position = CalculateRightHandPos();
        leftFootTarget.position = CalculateLeftFootPos();
        rightFootTarget.position = CalculateRightFootPos();

        leftHandIK.data.target.position = leftHandTarget.position;
        rightHandIK.data.target.position = rightHandTarget.position;
        leftFootIK.data.target.position = leftFootTarget.position;
        rightFootIK.data.target.position= rightFootTarget.position;

    }
    private void FixedUpdate()
    {
        if(GroundCheck())
        {
            Vector3 forceDirection = (targetPosition.position - centralRigidbody.position).normalized;
            Vector3 stabilizeDirection = (centerOfMass.position - transform.position).normalized;
            Vector3 error = targetPosition.position - centralRigidbody.position;
            integral += error * Time.fixedDeltaTime;
            Vector3 derivative = (error - lastError) / Time.fixedDeltaTime;
            lastError = error;

            Vector3 pidForce = moveForce * error +  stabilityForce * integral + 30 * derivative;

            centralRigidbody.AddForce(pidForce * Time.fixedDeltaTime);

        }

    }
    private void OnDrawGizmos()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            Gizmos.color = Color.red;
            Vector3 forceDirection = (targetPosition.position - rb.position).normalized;
            Gizmos.DrawLine(rb.position, rb.position + forceDirection * moveForce * 0.1f);

            Gizmos.color = Color.blue;
            Vector3 stabilityDirection = (centerOfMass.position - transform.position).normalized;
            Gizmos.DrawLine(rb.position, rb.position - stabilityDirection * stabilityForce * 0.1f);
        }
    }
    private Vector3 CalculateLeftHandPos()
    {
        return leftHandTarget.position;
    }
    private Vector3 CalculateRightHandPos()
    {
        return rightHandTarget.position;
    }
    private Vector3 CalculateLeftFootPos()
    {
        return leftFootTarget.position;
    }
    private Vector3 CalculateRightFootPos()
    {
        return rightFootTarget.position;
    }
    
    private bool GroundCheck()
    {
        float groundCheckDistance = 0.01f;
        foreach(Rigidbody rb in rigidbodies)
        {
            if (Physics.Raycast(rb.position, Vector3.down, groundCheckDistance))
            {
                return true;
            }
        }
        return false;
    }
    private void SetupJoints(ConfigurableJoint joint)
    {
        SoftJointLimit linearLimit = new SoftJointLimit();
        linearLimit.limit = 0.5f;
        joint.linearLimit = linearLimit;

        SoftJointLimitSpring angularSpring = new SoftJointLimitSpring();
        angularSpring.spring = 100;
        angularSpring.damper = 5f;
        joint.angularXLimitSpring = angularSpring;
        joint.angularYZLimitSpring = angularSpring;

        SoftJointLimit angularLimit = new SoftJointLimit();
        angularLimit.limit = 45f;
        joint.lowAngularXLimit = angularLimit;
        joint.highAngularXLimit = angularLimit;
        joint.angularYLimit = angularLimit;
        joint.angularZLimit = angularLimit;

        JointDrive drive = new JointDrive();
        drive.positionSpring = 100f;
        drive.positionDamper = 5f;
        joint.slerpDrive = drive;

        joint.anchor = Vector3.zero;
        joint.connectedAnchor = Vector3.zero;
        
        joint.targetPosition = transform.position;
        joint.targetRotation = transform.rotation;
    }
    private void IgnoreRagdollCollisions()
    {
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            for (int j = i + 1; j < rigidbodies.Length; j++)
            {
                Collider colliderA = rigidbodies[i].GetComponent<Collider>();
                Collider colliderB = rigidbodies[j].GetComponent<Collider>();

                if (colliderA != null && colliderB != null)
                {
                    Physics.IgnoreCollision(colliderA, colliderB);
                }
            }
        }
    }
}
