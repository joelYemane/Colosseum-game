using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullAi : MonoBehaviour
{
    public GameObject _lookatObject;

    private void OnTriggerEnter(Collider other)
    {
        _lookatObject = other.gameObject;

        Debug.LogWarning(_lookatObject.name);
    }
}
