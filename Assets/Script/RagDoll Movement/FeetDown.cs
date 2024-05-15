using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetDown : MonoBehaviour
{
    public float speed;
    public GameObject mainObject,newHeight;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mainObject.transform.position = Vector3.MoveTowards(mainObject.transform.position, newHeight.transform.position, speed);
        newHeight.transform.position = mainObject.transform.position;
    }
}
