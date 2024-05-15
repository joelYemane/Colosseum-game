using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    public float _timer, _rotationSpeed;
    public Transform _target;

    private void Update()
    {
        LookAt();
    }
    void LookAt()
    {
        _timer += Time.deltaTime;

        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    }
}
