using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boat : Receptacle
{
    [SerializeField] private string requiredIdentifier;
    [SerializeField] private float swayAmount;
    [SerializeField] private Transform seat;
    [SerializeField] private float endDuration, timeBeforeBoatMove;

    [SerializeField] private AK.Wwise.Event endCinematicEvent;

    private bool isActivated = false;

    private float endTimer;

    protected override void Start()
    {
        base.Start();
        transform.position += Vector3.down * swayAmount / 2f;
        transform.DOMoveY(transform.position.y + swayAmount, 3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (endTimer > 0)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0)
            {
                Debug.Log("END");
            }
        }
    }

    public override Vector3 Place(Item item)
    {
        if (item.identifier.Equals(requiredIdentifier))
        {
            SetBlocked(true);
            isActivated = true;
        }
        return base.Place(item);
    }

    public override void Use()
    {
        if (!isActivated)
        {
            // inspect
            HUDController.instance.DisplayText(TextType.Popup, inspectTextKey);
            ControlToggle.TakeControl(Close);
            CameraController.instance.ZoomTo(transform, 0.3f);
        }
        else
        {
            // end game
            ControlToggle.TakeControl(60f);
            HUDController.instance.ShowInputs(false);
            PlayerController.instance.transform.parent = seat;
            PlayerController.instance.transform.position = seat.position;
            PlayerController.instance.EndingState();
            endTimer = endDuration;
            Sequence boatMove = DOTween.Sequence();
            boatMove.AppendInterval(timeBeforeBoatMove);
            boatMove.Append(transform.DOMoveX(transform.position.x + 8f, 6f).SetEase(Ease.InQuad).OnStart(() =>
            {
                CameraController.instance.Freeze(true);
                HUDController.instance.Blackout(true, 5f);
            }));
        }
    }

    public override string GetUseTextKey()
    {
        if (!isActivated) return "action_inspect";
        else return "action_boat";
    }

    public override bool IsUseable()
    {
        return true;
    }
}