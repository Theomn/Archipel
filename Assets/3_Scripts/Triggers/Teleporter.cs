using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private AK.Wwise.Event zoneEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        other.transform.position = target.position;
        CameraController.instance.transform.position = PlayerController.instance.cameraTarget.position;
        CameraController.instance.Snap();
        zoneEvent.Post(gameObject);

    }
}
