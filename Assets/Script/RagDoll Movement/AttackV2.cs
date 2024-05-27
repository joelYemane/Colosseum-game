using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class AttackV2 : MonoBehaviour
{
    public Rigidbody arm;
   
    private Vector3 directionRightToLeft,directionUpDown,slamTowards;
    public float speed;
    public int randomIndex;
    public bool randomAttack = false;
    public Transform start,end;
    public GameObject player,sword;
    // Start is called before the first frame update
    void Start()
    {
        //Right to Left
       
        

    }

    // Update is called once per frame

    private void Update()
    {
        slamTowards = (sword.transform.position - player.transform.position).normalized;
       
        //arm.AddForceAtPosition(slamTowards,sword.transform.forward, ForceMode.Impulse);
    }
    void FixedUpdate()
    {
        arm.AddExplosionForce(speed, sword.transform.position, 10);

        //
        //if (!randomAttack)
        //{
        //    StartCoroutine(GetRandomAttack());
        //}

        //switch (randomIndex)
        //{
        //        case 0:
        //        SliceAttack(arm, slamTowards, speed);
        //        break;

        //        //case 1:
        //        //SliceAttack(arm, -directionRightToLeft, speed);
        //        //break;

        //}


    }

    //public IEnumerator GetRandomAttack()
    //{
    //    randomAttack = true;
    //    while (randomAttack)
    //    {
    //        yield return new WaitForSeconds(5);

    //        randomIndex = Random.Range(0, 2);
            
            
    //    }
       
    //}
    //public IEnumerator SlamPlayer()
    //{
    //    yield return new WaitForSeconds(1);
    //    speed = -2;
    //    yield return new WaitForSeconds(3);
    //    speed = 2;
    //    yield return null;
    //}
    //public void SliceAttack( Rigidbody rb, Vector3 direction, float speed)
    //{
        
    //    rb.AddForceAtPosition(direction * speed,arm.transform.position,ForceMode.Impulse);
    //    StartCoroutine(SlamPlayer());
    //}
}
