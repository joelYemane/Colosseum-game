using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class AttackV2 : MonoBehaviour
{
    public Rigidbody arm;
   
    private Vector3 slamTowards,playerV,enemyV;
    public float speed, distance;
    public int randomIndex;
    public bool randomAttack = false;
    public Transform start,end;
    public GameObject player,sword,enemy;
    // Start is called before the first frame update
    void Start()
    {
        
       
        

    }

    // Update is called once per frame

    private void Update()
    {
        playerV = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        enemyV = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
        slamTowards = (sword.transform.position - player.transform.position).normalized;
        distance =Vector3.Distance(enemyV , playerV);
    }
    void FixedUpdate()
    {
        if(distance < 4f)
        {
            arm.AddExplosionForce(speed, sword.transform.position, 10);
        }
      

        

    }

}
