using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemElevator : MonoBehaviour, Useable
{
    [SerializeField] private Transform receptacle;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;

    private bool isUp;
    private bool pending;

    private void Start()
    {
        receptacle.position = bottom.position;
    }

    public void Use()
    {
        if (pending) return;
        if (isUp) Lower();
        else Lift();
    }

    private void Lift()
    {
        pending = true;
        receptacle.DOKill();
        receptacle.DOMoveY(receptacle.position.y + 2f, 2f).SetEase(Ease.InSine).OnComplete(GoTo);
    }

    private void Lower()
    {
        pending = true;
        receptacle.DOKill();
        receptacle.DOMoveY(receptacle.position.y - 2f, 2f).SetEase(Ease.InSine).OnComplete(GoTo);
    }

    private void GoTo()
    {
        if (isUp) receptacle.position = bottom.position;
        else receptacle.position = top.position;
        isUp = !isUp;
        pending = false;
    }
}
