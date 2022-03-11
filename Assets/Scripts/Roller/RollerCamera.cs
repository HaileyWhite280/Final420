using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5;
    public float pitch = 45;
    public float sensitivity = 1;
    public float rate = 4;

    float yaw = 0;
    Vector3 targetPosition = Vector3.zero;

    private void Start()
    {
        targetPosition = target.position;
    }

    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        Quaternion qyaw = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion qpitch = Quaternion.AngleAxis(pitch, Vector3.right);
        Quaternion rotation = qyaw * qpitch;
        Vector3 offset = rotation * Vector3.back * distance;

        // interpolate to from target position to new target position
        targetPosition = Vector3.Lerp(targetPosition, target.position, rate * Time.deltaTime);

        transform.position = targetPosition + offset;
        transform.rotation = Quaternion.LookRotation(-offset);
    }
}
