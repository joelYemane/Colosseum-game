using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    public Transform headr;

  

    // Update is called once per frame
    void Update()
    {
        transform.position = headr.position;
    }
}
