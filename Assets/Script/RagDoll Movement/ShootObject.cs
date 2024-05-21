using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObject : MonoBehaviour
{
    public GameObject prefabShoot;
    public Transform shootPoint;
    public float ShootCooldown;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ShootBalls());
    }
    IEnumerator ShootBalls()
    {
        rb = prefabShoot.GetComponent<Rigidbody>();
        prefabShoot = Instantiate(prefabShoot,new Vector3(1,0,0),Quaternion.identity);
        rb.AddForce(0,0,1 ,ForceMode.Impulse);
        yield return new WaitForSeconds(ShootCooldown);
    }
}
