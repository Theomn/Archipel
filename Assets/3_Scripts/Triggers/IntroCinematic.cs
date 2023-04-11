using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCinematic : MonoBehaviour
{
    [SerializeField] private bool playOnStart;
    [SerializeField] private float duration;
    [SerializeField] private AK.Wwise.Event startEvent;
    private float timer;
    private HUDController hud;

    private readonly float fadeInDuration = 5f;

    private void Start()
    {
        if (!playOnStart)
        {
            Destroy(gameObject);
            return;
        }

        hud = HUDController.instance;
        startEvent.Post(gameObject);
        hud.Blackout(true);
        timer = duration;
        ControlToggle.TakeControl(duration + fadeInDuration);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            PlayerController.instance.StartState();
            hud.Blackout(false, fadeInDuration);
            Destroy(gameObject);
        }
    }
}
