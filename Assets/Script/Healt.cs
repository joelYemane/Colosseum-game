using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt : MonoBehaviour
{
    public float health;
    public float damage;
    public bool check;
    public float timer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0.1f)
        {
            gameObject.GetComponent<Animator>().SetBool("TakenDamage", false);
            timer = 1f;
            check = false;
        }
    }
    public void TakingDamage(float damage)
    {
        health -= damage;
        gameObject.GetComponent<Animator>().SetBool("TakenDamage", true);
        check = true;
        //gameObject.GetComponent<Material>().color = Color.yellow;
        Dying();
        
    }
    public void Dying() 
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
