using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableStation : MonoBehaviour, Useable
{
    [SerializeField] private string textKey;
    [SerializeField] private TextType textType;
    [SerializeField] private string thoughtKey;
    [SerializeField] private List<string> removeThoughtKeys;
    [SerializeField] private int eventNumberDiary;
    [SerializeField] private Transform highlightLocation;
    [SerializeField] private ParticleSystem unreadParticles;

    public void Use()
    {
        HUDController.instance.DisplayText(textType, textKey);

        if (eventNumberDiary != 0)
        {
            DiaryScreen.instance.revealText(eventNumberDiary);
        }
        
        ControlToggle.TakeControl(Close);
        CameraController.instance.ZoomTo(transform, 0.3f);

        unreadParticles.Stop();
    }

    public void Close()
    {
        HUDController.instance.CloseText(textType);
        CameraController.instance.ResetToPlayer();
        if (thoughtKey != "")
        {
            ThoughtScreen.instance.AddThought(thoughtKey);
        }
        foreach(var key in removeThoughtKeys)
        {
            ThoughtScreen.instance.RemoveThought(key);
        }
    }

    public Vector3 GetHighlightPosition()
    {
        return highlightLocation ? highlightLocation.position : transform.position;
    }
}
