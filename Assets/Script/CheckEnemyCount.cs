using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyCount : MonoBehaviour
{
    public GameObject shereCheck;
    public List<GameObject> enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        shereCheck = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        shereCheck.GetComponent<MeshRenderer>().enabled = false;
        shereCheck.GetComponent<Collider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        shereCheck.transform.position = transform.position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyCount.Add(other.gameObject);
            if(enemyCount.Count > 2) 
            {
                
            }
        }
    }
}
