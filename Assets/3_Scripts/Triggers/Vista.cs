using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vista : MonoBehaviour
{
    [SerializeField] private Transform vista;
    [SerializeField] private bool destroyOnExit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            CameraController.instance.ActivateVista(vista);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            CameraController.instance.ResetToPlayer();
            if (destroyOnExit) GetComponent<Collider>().enabled = false;
        }
    }
}
