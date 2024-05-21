using UnityEngine;
using System;

public class RaggdollHit : MonoBehaviour
{
    public WeakPointsColl _weakPointsColl = new WeakPointsColl();
    public WeakPointsJoint _weakPointsJoint = new WeakPointsJoint();    
    public WeakPointsBool _weakPointsBool = new WeakPointsBool();

    [Serializable]
    public class WeakPointsColl
    {
        public Collider _head;
        public Collider _armL;
        public Collider _armR;
        public Collider _leggL;
        public Collider _leggR;
    }

    [Serializable]
    public class WeakPointsJoint
    {
        public CharacterJoint _head;
        public CharacterJoint _armL;
        public CharacterJoint _armR;
        public CharacterJoint _leggL;
        public CharacterJoint _leggR;
    }

    [Serializable]
    public class WeakPointsBool
    {
        public bool _head;
        public bool _armL;
        public bool _armR;
        public bool _leggL;
        public bool _leggR;
    }

    private void FixedUpdate()
    {
        if (_weakPointsBool._head && _weakPointsJoint._head)
        {
            GameObject _obj = _weakPointsJoint._head.gameObject;
            Destroy(_weakPointsJoint._head);

            _obj.transform.parent = null;
            _obj.GetComponent<Rigidbody>().excludeLayers += 3;
            _obj.GetComponent<Rigidbody>().excludeLayers += 6;

            UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable _interactlow = _obj.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
            _interactlow.interactionLayers = 3;
            _interactlow.useDynamicAttach = true;
        }

        if (_weakPointsBool._armL && _weakPointsJoint._armL)
        {
            GameObject _obj = _weakPointsJoint._armL.gameObject;
            Destroy(_weakPointsJoint._armL);

            _obj.transform.parent = null;
            _obj.GetComponent<Rigidbody>().excludeLayers += 3;
            _obj.GetComponent<Rigidbody>().excludeLayers += 6;
            
            UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable _interactlow = _obj.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
            _interactlow.interactionLayers = 3;
            //_interactlow.useDynamicAttach = true;
        }

        if (_weakPointsBool._armR && _weakPointsJoint._armR)
        {
            GameObject _obj = _weakPointsJoint._armR.gameObject;
            Destroy(_weakPointsJoint._armR);

            _obj.transform.parent = null;
            _obj.GetComponent<Rigidbody>().excludeLayers += 3;
            _obj.GetComponent<Rigidbody>().excludeLayers += 6;

            UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable _interactlow = _obj.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
            _interactlow.interactionLayers = 3;
            //_interactlow.useDynamicAttach = true;
        }

        if (_weakPointsBool._leggL && _weakPointsJoint._leggL)
        {
            GameObject _obj = _weakPointsJoint._leggL.gameObject;
            Destroy(_weakPointsJoint._leggL);

            _obj.transform.parent = null;
            _obj.GetComponent<Rigidbody>().excludeLayers += 3;
            _obj.GetComponent<Rigidbody>().excludeLayers += 6;

            UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable _interactlow = _obj.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
            _interactlow.interactionLayers = 3;
            //_interactlow.useDynamicAttach = true;
        }

        if (_weakPointsBool._leggR && _weakPointsJoint._leggR)
        {
            GameObject _obj = _weakPointsJoint._leggR.gameObject;
            Destroy(_weakPointsJoint._leggR);

            _obj.transform.parent = null;
            _obj.GetComponent<Rigidbody>().excludeLayers += 3;
            _obj.GetComponent<Rigidbody>().excludeLayers += 6;

            UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable _interactlow = _obj.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
            _interactlow.interactionLayers = 3;
            //_interactlow.useDynamicAttach = true;
        }
    }
}