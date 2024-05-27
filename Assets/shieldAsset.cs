using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldAsset : MonoBehaviour
{
    public Transform player, enemy, arm;
    public float offSet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (enemy.position - player.position).normalized;
        direction.y -=  -offSet;
        arm.transform.LookAt(direction);
    }
}
