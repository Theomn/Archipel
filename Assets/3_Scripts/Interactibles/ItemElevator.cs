using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemElevator : MonoBehaviour, Useable
{
    [SerializeField] private Transform receptacle;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;
    [SerializeField] private List<Transform> ropes;

    [SerializeField] private AK.Wwise.Event startEvent;
    [SerializeField] private AK.Wwise.Event endEvent;

    private bool isUp;
    private bool pending;

    private Sequence liftSequence, lowerSequence;

    private Vector3 upFwd = Vector3.up + Vector3.forward;

    private void Start()
    {
        receptacle.position = bottom.position;

        foreach (var rope in ropes)
        {
            Utils.SetHighSprite(rope, 0.4f);
            rope.position += upFwd * 0.4f;
        }

        liftSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        liftSequence.Append(receptacle.DOMove(bottom.position + upFwd * 3f, 2f).SetEase(Ease.InSine))
        .Append(receptacle.DOMove(top.position - upFwd * 3f, 0.01f))
        .Append(receptacle.DOMove(top.position, 2f).SetEase(Ease.OutSine))
        .OnComplete(ToggleState);

        lowerSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        lowerSequence.Append(receptacle.DOMove(top.position - upFwd * 3f, 2f).SetEase(Ease.InSine))
        .Append(receptacle.DOMove(bottom.position + upFwd * 3f, 0.01f))
        .Append(receptacle.DOMove(bottom.position, 2f).SetEase(Ease.OutSine))
        .OnComplete(ToggleState);
    }

    public void Use()
    {
        if (pending) return;

        foreach (var rope in ropes)
            rope.DOMoveY(rope.position.y - 0.2f, 0.3f).SetLoops(2, LoopType.Yoyo);
        startEvent.Post(gameObject);
        if (isUp) Lower();
        else Lift();
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
        pending = false;
        endEvent.Post(gameObject);
    }
}
