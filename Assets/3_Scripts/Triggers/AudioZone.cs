using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event zoneEvent;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != Layer.player) return;
        
        zoneEvent.Post(gameObject);
    }
}