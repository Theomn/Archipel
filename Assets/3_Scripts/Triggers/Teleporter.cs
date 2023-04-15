using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool goesInside;
    [SerializeField] private AK.Wwise.Event zoneEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        HUDController.instance.Blackout(true, Teleport);
        ControlToggle.TakeControl(0.6f);
    }

    private void Teleport()
    {
        PlayerController.instance.SetInside(goesInside);
        PlayerController.instance.transform.position = target.position;
        CameraController.instance.transform.position = PlayerController.instance.cameraTarget.position;
        CameraController.instance.Snap();
        zoneEvent.Post(gameObject);
        HUDController.instance.Blackout(false);
    }
}
