using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollController : MonoBehaviour
{
    public ConfigurableJoint[] configurableJoint;
    private Vector3 goToBody;
    private Vector3 targetPos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        configurableJoint = GetComponentsInChildren<ConfigurableJoint>();

        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 goToBody = (transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position,targetPos);
        if(distance >0.1f)
        {
            float movementAmount = speed *Time.deltaTime;
            transform.position += goToBody * movementAmount;
             foreach (ConfigurableJoint joint in configurableJoint)
            {
                joint.targetPosition = transform.InverseTransformPoint(targetPos);
            }
        }
       
    }
    public void SetTargetPosition(Vector3 newPosition)
    {
        targetPos = newPosition;
    }
}
