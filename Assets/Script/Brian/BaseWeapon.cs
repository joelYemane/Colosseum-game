using UnityEngine;
using EzySlice;
using System;

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


    [Header("")]
    public SliceClass _slice = new SliceClass();

    [Serializable]
    public class SliceClass
    {
        public Material _inside;
        public Transform startSlicePoint;
        public Transform endSlicePoint;
        public VelocityEstimator velocityEstimator;
        public LayerMask sliceableLayer;
        public float cutForce = 50f;
    }

    public void Holding()
    {
        //_holdingLayer = _hoverLayer;
        Debug.LogWarning("Pickup");

        GetComponent<Collider>().excludeLayers = _holdingLayer;
    }

    public void drop()
    {
        Debug.LogWarning("Drop");

        GetComponent<Collider>().excludeLayers -= _holdingLayer;
        _holdingLayer = ~0;
    }

    void FixedUpdate()
    {
        bool hadHit = Physics.Linecast(_slice.startSlicePoint.position, _slice.endSlicePoint.position, out RaycastHit hit, _slice.sliceableLayer);
        if (hadHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
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

    void Slice(GameObject target)
    {
        Vector3 velocity = _slice.velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(_slice.endSlicePoint.position - _slice.startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull slice = target.Slice(_slice.endSlicePoint.position, planeNormal);

        if(slice != null)
        {
            GameObject upperHull = slice.CreateUpperHull(target, _slice._inside);
            SetSlice(upperHull);
            GameObject lowerHull = slice.CreateLowerHull(target, _slice._inside);
            SetSlice(lowerHull);

            Destroy(target);
        }
    }

    void SetSlice(GameObject Hull)
    {
        Hull.AddComponent<Rigidbody>();
        Hull.AddComponent<MeshCollider>().convex = true;
        Hull.AddComponent<MeshFilter>();
        UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable _interactlow = Hull.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();

        _interactlow.interactionLayers = 3;
        _interactlow.useDynamicAttach = true;
    }
}