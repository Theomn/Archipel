using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonStation : UseableStation
{
    [SerializeField] private float cooldown;

    [SerializeField] private AK.Wwise.Event pressEvent, wrongEvent;

    private float timer;

    private SpriteRenderer sprite;

    private bool pending;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public override void Use()
    {
        if (pending || timer > 0)
        {
            NegativeFeedback();
            return;
        }

        base.Use();
        timer = cooldown;
        GetComponentInChildren<Animator>().SetTrigger("Press");
        pressEvent.Post(gameObject);
    }

    public void SetPending(bool active)
    {
        pending = active;
    }

    private void NegativeFeedback()
    {
        sprite.DOKill();
        sprite.transform.DOKill();
        sprite.color = new Color(1, 0.5f, 0.5f, 1);
        sprite.DOColor(Color.white, 0.3f);
        sprite.transform.DOPunchPosition(Vector3.back * 0.1f, 0.5f);
        wrongEvent.Post(gameObject);
    }
}
