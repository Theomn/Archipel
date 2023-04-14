using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EdibleItem : Item
{
    [SerializeField] private string modifier;
    [SerializeField] private float modifierDuration;
    [SerializeField] private AK.Wwise.Event eatEvent;
    private float animationDuration = 0.6f;
    private PlayerItem player;

    protected override void Awake()
    {
        base.Awake();
        isUseable = true;
    }

    protected override void Start()
    {
        base.Start();
        player = PlayerItem.instance;
        
    }

    public override void Use()
    {
        if (PlayerController.instance.state != PlayerController.State.Idle) return;

        base.Use();
        PlayerModifiers.instance.AddModifier(modifier, modifierDuration > 0 ? modifierDuration : -100f);
        player.RemoveItem();
        ControlToggle.TakeControl(3f);
        float handsMouthDistance = Vector3.Distance(player.mouth.localPosition, player.initialHandsPosition);
        transform.DOMoveY(transform.position.y + handsMouthDistance, animationDuration).SetEase(Ease.OutSine);
        transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InCubic).onKill += () => Destroy();
    }

    private void Destroy()
    {
        ControlToggle.Unpause();
        transform.DOKill();
        eatEvent.Post(gameObject);
        Destroy(gameObject);
    }
}
