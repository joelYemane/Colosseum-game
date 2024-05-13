using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public string _name;
    public string _discription;

    public float _summonForge;
    public float _summonDelay;
    public GameObject _magicObject;

    bool _holdingL;
    bool _holdingR;

    bool _canSummon;

    Vector3 _direction;

    XRIDefaultInputActions _input;

    Vector3 _speed;

    private void Awake()
    {
        _input = new XRIDefaultInputActions();
    }

    private void Start()
    {
        _canSummon = true;
    }
                                                                       
    private void FixedUpdate()
    {
        if (_speed.x >= _summonForge && _canSummon)
        {
            Quaternion _lastDirection = new Quaternion(_direction.x, _direction.y, _direction.z, 0);
            Instantiate(_magicObject, transform.position, _lastDirection);

            _canSummon = false;
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(_summonDelay);

        _canSummon = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        _direction = collision.transform.forward;

        //if(collision.transform.tag == "Left")
        //{
        //    _holdingL = true;
        //    _holdingR = false;
        //}

        //else if (collision.transform.tag == "Right")
        //{
        //    _holdingL = false;
        //    _holdingR = true;
        //}
    }
}
