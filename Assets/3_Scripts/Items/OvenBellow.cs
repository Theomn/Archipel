using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OvenBellow : MonoBehaviour
{
    [SerializeField] private Oven oven;
    [SerializeField] private Transform bellow;

    private Vector3 initialScale;
    private Sequence blowSequence;

    private bool pending;

    private void Awake() {
        initialScale = transform.localScale;
        blowSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        blowSequence.Append(bellow.DOScaleY(initialScale.y * 0.1f, 0.2f));
        blowSequence.Append(bellow.DOScaleY(initialScale.y, 1.2f).OnComplete(() => pending = false));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layer.player) return;
        if (pending) return;

        pending = true;
        oven.Blow();
        blowSequence.Restart();
    }
}
