using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackV2 : MonoBehaviour
{
    public Rigidbody arm;
    public Transform ikTarget;
    private Vector3 direction;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        direction = Quaternion.Euler(0,45,0)*transform.right;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        arm.AddForce(-direction * speed);
    }
}
