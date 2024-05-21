using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetcullingMatrix : MonoBehaviour
{
    Camera _cam;
    private void Start()
    {
        _cam = GetComponent<Camera>();

        _cam.cullingMatrix = Matrix4x4.Ortho(-99999, 99999, -99999, 99999, 0.001f, 99999) *
                            Matrix4x4.Translate(Vector3.forward * -99999 / 2f) *
                            _cam.worldToCameraMatrix;
    }
}
