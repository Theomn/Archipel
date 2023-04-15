using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        PlayerController.instance.SetOnWater(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;

        PlayerController.instance.SetOnWater(false);
    }
}
