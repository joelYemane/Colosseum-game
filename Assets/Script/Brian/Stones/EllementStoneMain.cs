using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllementStoneMain : MonoBehaviour
{
    public EllementStoneScripObject _ellement;

    bool _holding;

    public void Holding(bool _bool)
    {
        _holding = _bool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BaseWeapon>() && _holding)
        {
            collision.gameObject.GetComponent<BaseWeapon>()._upgrade = _ellement;
            collision.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", _ellement._EmissionColor);

            Destroy(gameObject);
        }
    }
}
