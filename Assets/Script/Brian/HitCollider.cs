using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    public BaseWeapon _weapon;

    private void OnCollisionEnter(Collision collision)
    {
        _weapon._hitloc = collision.GetContact(0).point;
        _weapon.Hitcollider(collision.gameObject);
        _weapon.Hit(collision.collider);

    }
}
