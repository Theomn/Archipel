using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            other.transform.position = target.position;
            CameraController.instance.transform.position = PlayerController.instance.cameraTarget.position;
        }
    }
}
