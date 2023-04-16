using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemElevator : Event
{
    [SerializeField] private Transform receptacle;
    [SerializeField] private Transform top;
    [SerializeField] private GameObject topBlocker;
    [SerializeField] private Transform bottom;
    [SerializeField] private GameObject bottomBlocker;
    [SerializeField] private List<ButtonStation> buttons;
    [SerializeField] private AK.Wwise.Event startEvent;
    [SerializeField] private AK.Wwise.Event endEvent;

    private bool isUp;
    private bool pending;

    private Sequence liftSequence, lowerSequence;

    private readonly Vector3 upFwd = Vector3.up + Vector3.forward;
    private readonly float animHeight = 2f;
    private readonly float animLength = 1f;

    private void Awake()
    {
        receptacle.position = bottom.position;
        bottomBlocker?.SetActive(false);
        topBlocker?.SetActive(true);
    }

    private void Start()
    {

        liftSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        liftSequence.Append(receptacle.DOMove(bottom.position + upFwd * animHeight, animLength).SetEase(Ease.InSine))
        .Append(receptacle.DOMove(top.position - upFwd * animHeight, 0.001f))
        .Append(receptacle.DOMove(top.position, animLength).SetEase(Ease.OutSine))
        .OnComplete(ToggleState);

        lowerSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        lowerSequence.Append(receptacle.DOMove(top.position - upFwd * animHeight, animLength).SetEase(Ease.InSine))
        .Append(receptacle.DOMove(bottom.position + upFwd * animHeight, 0.001f))
        .Append(receptacle.DOMove(bottom.position, animLength).SetEase(Ease.OutSine))
        .OnComplete(ToggleState);
    }

    public override void Activate()
    {
        if (pending) return;

        foreach (var button in buttons)
        {
            button.SetPending(true);
        }
        startEvent.Post(gameObject);
        bottomBlocker?.SetActive(true);
        topBlocker?.SetActive(true);
        if (isUp) Lower();
        else Lift();
    }

    public override void Deactivate()
    {
        return;
    }

    private void Lift()
    {
        pending = true;
        liftSequence.Restart();
    }

    private void Lower()
    {
        pending = true;
        lowerSequence.Restart();
    }

    private void ToggleState()
    {
        isUp = !isUp;
        if (isUp) topBlocker?.SetActive(false);
        else bottomBlocker?.SetActive(false);
        pending = false;
        foreach (var button in buttons)
        {
            button.SetPending(false);
        }
        endEvent.Post(gameObject);
    }
}
