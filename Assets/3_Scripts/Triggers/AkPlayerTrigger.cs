using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkPlayerTrigger : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AkTriggerEnter>().triggerObject = PlayerController.instance.gameObject;
    }
}
