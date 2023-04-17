using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vista : MonoBehaviour
{
    [SerializeField] private float smooth;
    [SerializeField] private Transform vista;
    [SerializeField] private float duration;
    [SerializeField] private bool destroyOnExit;

    private float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        timer = duration;
        CameraController.instance.ActivateVista(vista, smooth);
        if (duration > 0) GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                CameraController.instance.ResetToPlayer();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;
        if (duration > 0) return; // exiting vista does not stop it if it is timed
        
        CameraController.instance.ResetToPlayer();
        if (destroyOnExit) GetComponent<Collider>().enabled = false;

    }
}
