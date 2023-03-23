using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEvent : Event
{
    [SerializeField] private GameObject onGroup, offGroup;

    private void Start()
    {
        Deactivate();
    }

    public override void Activate()
    {
        onGroup.SetActive(true);
        offGroup.SetActive(false);
    }

    public override void Deactivate()
    {
        onGroup.SetActive(false);
        offGroup.SetActive(true);
    }
}
