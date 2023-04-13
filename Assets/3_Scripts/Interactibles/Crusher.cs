using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crusher : Transformer
{
    [SerializeField] private Transform mainStone;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private List<string> removeThoughtKeys;


    [Header("Wwise")]
    [SerializeField] private AK.Wwise.Event riseEvent;
    [SerializeField] private AK.Wwise.Event fallEvent, crushSuccessfulEvent, crushUnsuccessfulEvent;
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
        riseEvent.Post(gameObject);
    }

    public void Fall()
    {
        isUp = false;
        receptacle.SetBlocked(true);
        mainStone.DOKill();
        mainStone.DOMove(initialPosition, 0.2f).SetEase(Ease.InQuad)
        .OnComplete(Land);
        fallEvent.Post(gameObject);
    }

    private void Land()
    {
        CameraController.instance.Shake();
        particles.Play();

        foreach (var key in removeThoughtKeys)
        {
            ThoughtScreen.instance.RemoveThought(key);
        }

        if (Transform()) crushSuccessfulEvent.Post(gameObject);
        else crushUnsuccessfulEvent.Post(gameObject);
    }

    private void Hover()
    {
        mainStone.DOMoveY(mainStone.transform.position.y - 0.12f, 1.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
