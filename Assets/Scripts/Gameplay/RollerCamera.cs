using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5;
    public float pitch = 45;
    public float sensitivity = 1;

    float yaw = 0;

    // Update is called once per frame
    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;

        Quaternion qyaw = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion qpitch = Quaternion.AngleAxis(pitch, Vector3.right);
        Quaternion rotation = qyaw * qpitch;
        Vector3 offset = rotation * Vector3.back * distance;

        transform.position = target.position + offset;
        rotation = Quaternion.LookRotation(offset);
    }
}
