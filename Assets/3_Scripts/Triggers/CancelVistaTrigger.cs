using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelVistaTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        CameraController.instance.CancelVista();
    }
}
