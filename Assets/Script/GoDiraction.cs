using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDiraction : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(-transform.forward * 250, ForceMode.Acceleration);
    }
}
