using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetDown : MonoBehaviour
{
    
    public GameObject feetL,feetR;
    private Rigidbody feetLRB,feetRRB;
    public float power;
    // Start is called before the first frame update
    void Start()
    {
        feetLRB= feetL.GetComponent<Rigidbody>();
        feetRRB= feetR.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyForceDown(feetL,power);
        ApplyForceDown(feetR,power);
    }
    void ApplyForceDown(GameObject feet, float power)
    {
        Rigidbody feetRB = feet.GetComponent<Rigidbody>();
        feetRB.AddForce(Vector3.down *  power);

    }
}
