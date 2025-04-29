using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float MinAngle = -45f;
    public float MaxAngle = 45f;

    public float RotationSpeed = 10f;

    private void FixedUpdate()
    {
        float angle = Mathf.PingPong(Time.time * RotationSpeed, MaxAngle - MinAngle) + MinAngle;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z);
    }
}
