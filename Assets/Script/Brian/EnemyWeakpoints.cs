using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakpoints : MonoBehaviour
{

    public bool _autoInport;

    public EllementStoneScripObject _elementWeakness;

    private void Start()
    {
        if (_autoInport)
        {
            _weakpoints[0] = GetComponent<Collider>();
            _weakpointsHealth[0] = 1;
        }
    }
    public Collider[] _weakpoints;
    public float[] _weakpointsHealth;
}
