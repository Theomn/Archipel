using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vista : MonoBehaviour
{
    [SerializeField] private float smooth;
    [SerializeField] private Transform vista;
    [SerializeField] private bool destroyOnExit;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            CameraController.instance.ActivateVista(vista, smooth);
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
