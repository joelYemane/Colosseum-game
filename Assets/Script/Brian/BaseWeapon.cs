using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public enum WeaponType
{
    Sword,
    Axe,
    Spear,
    Hammer
}

public class BaseWeapon : MonoBehaviour
{
    [Header("Weapon info")]
    public string _name;
    public string _description;
    public WeaponType _type;

    [Header("Weapon stats")]
    public float _damage;
    public float _weight;

    [Header("Weapon Extra")]
    public LayerMask _upgradeLayer;


    [HideInInspector] public bool _holding;
    LayerMask _holdingLayer;
    public Transform _blade;

    private void Start()
    {
        _blade = GetComponentInChildren<MeshCollider>().transform;
    }

    public void Holding(LayerMask _layer)
    {
        _holdingLayer = _layer;
    }

    public void drop()
    {
        _holdingLayer = ~0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == _upgradeLayer)
        {


            return;
        }

        if(other.gameObject.layer != _holdingLayer)
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        slide(other.gameObject);
    }

    void slide(GameObject target)
    {
        SlicedHull slice = target.Slice(_blade.position, _blade.up);

        if(slice != null)
        {
            GameObject upperHull = slice.CreateUpperHull(target);
            GameObject lowerHull = slice.CreateLowerHull(target);

            Destroy(target);

            upperHull.AddComponent<Rigidbody>();
            upperHull.AddComponent<MeshCollider>().convex = true;
            upperHull.layer = 8;
            upperHull.AddComponent<MeshFilter>();

            lowerHull.AddComponent<Rigidbody>();
            lowerHull.AddComponent<MeshCollider>().convex = true;
            lowerHull.layer = 8;
            lowerHull.AddComponent<MeshFilter>();
        }
    }
}
