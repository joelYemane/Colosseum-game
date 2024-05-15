using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    public BaseWeapon _weapon;

    public LayerMask _enemyLayer;

    private void OnCollisionEnter(Collision collision)
    {
        _weapon._hitloc = collision.GetContact(0).point;
        _weapon.Hitcollider(collision.gameObject);
        _weapon.Hit(collision.collider);

        if(collision.gameObject.layer == _enemyLayer)
        {
            _weapon.EnemyHit(collision.gameObject);
        }
    }
}
