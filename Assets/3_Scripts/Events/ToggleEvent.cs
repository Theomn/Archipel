using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEvent : Event
{
    [SerializeField] private GameObject onGroup, offGroup;

    [SerializeField] private AK.Wwise.Event onEvent, offEvent;

    private void Start()
    {
        Deactivate();
    }

    public override void Activate()
    {
        onGroup.SetActive(true);
        offGroup.SetActive(false);
        onEvent.Post(gameObject);
    }

    public override void Deactivate()
    {
        onGroup.SetActive(false);
        offGroup.SetActive(true);
        offEvent.Post(gameObject);
    }
}
