using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vista : MonoBehaviour
{
    [SerializeField] private Transform vista;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            CameraController.instance.ActivateVista(vista.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            CameraController.instance.DeactivateVista();
        }
    }
}
