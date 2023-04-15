using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] private string textKey;
    [SerializeField] private TextType textType;
    [SerializeField] private bool destroyOnTrigger;
    [SerializeField] private Transform zoomTarget;
    [SerializeField] private string thoughtKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layer.player)
        {
            HUDController.instance.DisplayText(textType, textKey);
            ControlToggle.TakeControl(Close);
            if (zoomTarget) CameraController.instance.ZoomTo(zoomTarget);
            if (destroyOnTrigger) GetComponentInChildren<Collider>().enabled = false;
        }
    }

    public void Close()
    {
        HUDController.instance.CloseText(textType);
        CameraController.instance.ResetToPlayer();
        if (thoughtKey != "")
        {
            ThoughtScreen.instance.AddThought(thoughtKey);
        }
    }
}
