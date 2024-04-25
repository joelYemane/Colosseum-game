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
    public float _sharpnes;
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
        [HideInInspector] public Material _inside;
        public SlideMaterials _sliceMaterials = new SlideMaterials();
        public Transform startSlicePoint;
        public Transform endSlicePoint;
        public VelocityEstimator velocityEstimator;
        public LayerMask sliceableLayer;
        public float cutForce = 50f;

        [Serializable]
        public class SlideMaterials
        {
            public Material _wood;
            public Material _metal;
            public Material _enemy;
        }
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

    void Update()
    {
        bool hadHit = Physics.Linecast(_slice.startSlicePoint.position, _slice.endSlicePoint.position, out RaycastHit hit, _slice.sliceableLayer);

        if (hadHit && hit.collider != null)
        {
            Hit(hit.collider);
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

    private void Hit(Collider Hitcoll)
    {
        EnemyWeakpoints enemy = Hitcoll.GetComponent<EnemyWeakpoints>();

        for (int i = 0; i < enemy._weakpoints.Length; i++)
        {
            if (Hitcoll == enemy._weakpoints[i])
            {
                enemy._weakpointsHealth[i] -= _sharpnes;

                if (enemy._weakpointsHealth[i] <= 0)
                {
                    Slice(Hitcoll.gameObject);
                }
            }
        }


    }


    void Slice(GameObject target)
    {
        if(target.tag == "WoodTexture")
        {
            _slice._inside = _slice._sliceMaterials._wood;
        }

        else if (target.tag == "MetalTexture")
        {
            _slice._inside = _slice._sliceMaterials._metal;
        }

        else if (target.tag == "EnemyTexture")
        {
            _slice._inside = _slice._sliceMaterials._enemy;
        }

        else
        {
            _slice._inside = _slice._sliceMaterials._metal;
        }

        Vector3 velocity = _slice.velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(_slice.endSlicePoint.position - _slice.startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull slice = target.Slice(_slice.endSlicePoint.position, planeNormal);

        if(slice != null)
        {
            GameObject upperHull = slice.CreateUpperHull(target, _slice._inside);
            SetSlice(upperHull, target);
            GameObject lowerHull = slice.CreateLowerHull(target, _slice._inside);
            SetSlice(lowerHull, target);

            Destroy(target);
        }
    }

    void SetSlice(GameObject Hull, GameObject Parent)
    {
        Hull.tag = Parent.tag;

        Hull.AddComponent<Rigidbody>().excludeLayers += 6;
        Hull.AddComponent<MeshCollider>().convex = true;
        Hull.AddComponent<MeshFilter>();
        UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable _interactlow = Hull.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();

        _interactlow.interactionLayers = 3;
        _interactlow.useDynamicAttach = true;
    }
}