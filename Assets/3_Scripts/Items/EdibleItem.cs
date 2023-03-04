using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EdibleItem : Item
{
    [SerializeField] private string modifier;
    [SerializeField] private float modifierDuration;
    private float animationDuration = 0.6f;
    private float destroyTimer;
    private PlayerItem player;

    protected override void Start() {
        base.Start();
        player = PlayerItem.instance;
    }

    public override void Use()
    {
        base.Use();
        PlayerModifiers.instance.AddModifier(modifier, modifierDuration > 0 ? modifierDuration : -100f);
        player.RemoveItem();
        PlayerController.instance.Pause(true);
        float handsMouthDistance = Vector3.Distance(player.mouth.localPosition, player.initialHandsPosition);
        transform.DOMoveY(transform.position.y + handsMouthDistance, animationDuration).SetEase(Ease.OutSine);
        //transform.DOMove(player.mouth.position, animationDuration).SetEase(Ease.OutSine);
        transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InCubic);
        destroyTimer = animationDuration;
    }

    protected override void Update()
    {
        base.Update();
        if (destroyTimer > 0)
        {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer <= 0)
            {
                PlayerController.instance.Pause(false);
                transform.DOKill();
                Destroy(gameObject);
            }
        }
    }
}
