using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float _damage;

    public GameObject _explosion;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_explosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
