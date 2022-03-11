using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ForceEffector : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] [Range(0, 100)] float magnitude;
    [SerializeField] ForceMode forceMode;

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            rigidbody.AddForce(direction * magnitude, forceMode);
        }
    }

}
