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
        bottomBlocker.SetActive(false);
        topBlocker.SetActive(true);

        foreach (var rope in ropes)
        {
            Utils.SetHighSprite(rope, 0.4f);
            rope.position += upFwd * 0.4f;
        }

        liftSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        liftSequence.Append(receptacle.DOMove(bottom.position + upFwd * 3f, 2f).SetEase(Ease.InSine))
        .Append(receptacle.DOMove(top.position - upFwd * 3f, 0.001f))
        .Append(receptacle.DOMove(top.position, 2f).SetEase(Ease.OutSine))
        .OnComplete(ToggleState);

        lowerSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        lowerSequence.Append(receptacle.DOMove(top.position - upFwd * 3f, 2f).SetEase(Ease.InSine))
        .Append(receptacle.DOMove(bottom.position + upFwd * 3f, 0.001f))
        .Append(receptacle.DOMove(bottom.position, 2f).SetEase(Ease.OutSine))
        .OnComplete(ToggleState);
    }

    public override void Activate()
    {
        if (pending)
        {
            foreach (var rope in ropes)
            {
                var sprite = rope.GetComponentInChildren<SpriteRenderer>();
                sprite.color = new Color(1, 0.5f, 0.5f, 1);
                sprite.DOKill();
                sprite.DOColor(Color.white, 0.2f);
                sprite.transform.DORestart();
                sprite.transform.DOKill();
                sprite.transform.DOPunchPosition(Vector3.down * 0.1f, 0.5f);
            }
            return;
        }


        foreach (var rope in ropes)
            rope.DOMoveY(rope.position.y - 0.4f, 0.2f).SetLoops(2, LoopType.Yoyo);
        startEvent.Post(gameObject);
        bottomBlocker.SetActive(true);
        topBlocker.SetActive(true);
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
        if (isUp) topBlocker.SetActive(false);
        else bottomBlocker.SetActive(false);
        pending = false;
        endEvent.Post(gameObject);
    }
}
