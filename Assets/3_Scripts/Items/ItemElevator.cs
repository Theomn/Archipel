using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemElevator : MonoBehaviour, Useable
{
    [SerializeField] private Transform receptacle;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;
    [SerializeField] private Transform rope;

    private bool isUp;
    private bool pending;

    private Sequence liftSequence, lowerSequence;

    private void Start()
    {
        receptacle.position = bottom.position;

        Utils.SetHighSprite(rope, 1);
        rope.position += Vector3.up + Vector3.forward;

        liftSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        liftSequence.Append(receptacle.DOMoveY(bottom.position.y + 3f, 2f).SetEase(Ease.InSine))
        .Append(receptacle.DOMove(top.position - Vector3.up * 3f, 0.01f))
        .Append(receptacle.DOMoveY(top.position.y, 2f).SetEase(Ease.OutSine))
        .OnComplete(ToggleState);

        lowerSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        lowerSequence.Append(receptacle.DOMoveY(top.position.y - 3f, 2f).SetEase(Ease.InSine))
        .Append(receptacle.DOMove(bottom.position + Vector3.up * 3f, 0.01f))
        .Append(receptacle.DOMoveY(bottom.position.y, 2f).SetEase(Ease.OutSine))
        .OnComplete(ToggleState);
    }

    public void Use()
    {
        if (pending) return;

        rope.DOMoveY(rope.position.y - 0.2f, 0.3f).SetLoops(2, LoopType.Yoyo);
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
    }
}
