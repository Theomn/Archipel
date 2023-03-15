using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crusher : Transformer
{
    [SerializeField] private Transform mainStone;
    [SerializeField] private ParticleSystem particles;
    private Vector3 initialPosition;
    public bool isUp { get; private set; }
    private float riseHeight = 0.8f;

    private void Awake()
    {
        initialPosition = transform.position;
        isUp = false;
        receptacle.SetBlocked(true);
    }
    public void Rise()
    {
        isUp = true;
        receptacle.SetBlocked(false);
        mainStone.DOKill();
        mainStone.DOMove(initialPosition + Vector3.up * riseHeight, 1f).SetEase(Ease.InOutSine).OnComplete(Hover);
    }

    public void Fall()
    {
        isUp = false;
        receptacle.SetBlocked(true);
        mainStone.DOKill();
        mainStone.DOMove(initialPosition, 0.2f).SetEase(Ease.InQuad)
        .OnComplete(Land);
    }

    private void Land()
    {
        CameraController.instance.Shake();
        particles.Play();
        Activate();
    }

    private void Hover()
    {
        mainStone.DOMoveY(mainStone.transform.position.y - 0.12f, 1.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
