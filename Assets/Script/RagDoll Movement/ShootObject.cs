using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObject : MonoBehaviour
{
    public GameObject prefabShoot;
    public Transform shootPoint;
    public float ShootCooldown;
    public Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootCooldown -= Time.deltaTime;
        if(ShootCooldown <= 0)
        {
            StartCoroutine(ShootBalls());
            ShootCooldown = 4;
        }
        
    }
    IEnumerator ShootBalls()
    {
      
        GameObject cannonball = Instantiate(prefabShoot,shootPoint.position,Quaternion.identity);
        rb = cannonball.GetComponent<Rigidbody>();
        rb.AddForce(shootPoint.transform.forward * speed ,ForceMode.Impulse);
        yield return new WaitForSeconds(ShootCooldown);
    }
}
