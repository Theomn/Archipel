using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private float duration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            HUDController.instance.DisplaySubtitle(key, duration);
            Destroy(gameObject);
        }
    }
}
