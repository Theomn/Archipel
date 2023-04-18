using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event zoneEvent;
    [SerializeField] private bool destroyOnTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != Layer.player) return;
        
        zoneEvent.Post(gameObject);

        if (destroyOnTrigger) gameObject.SetActive(false);
    }
}