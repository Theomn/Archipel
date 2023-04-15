using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Telescope : Receptacle, Useable
{
    [SerializeField] private int horizontalGoal;
    [SerializeField] private int verticalGoal;
    [SerializeField] private Event triggeredEvent;
    [SerializeField] private float sensitivity;
    [SerializeField] private string lensIdentifier;
    [SerializeField] private TMP_Text horizontalText;
    [SerializeField] private TMP_Text verticalText;
    [SerializeField] private Transform zoomTarget;
    [SerializeField] private AK.Wwise.Event tickEvent;
    private float horizontal = 36, vertical = 61;

    private bool goalReached;

    private void Start()
    {
        UpdateText(horizontalText, horizontal);
        UpdateText(verticalText, vertical);
    }

    public override void Use()
    {
        ControlToggle.TakeControl(Close);
        CameraController.instance.ZoomTo(zoomTarget, 0, 3f);
    }

    private void Close()
    {
        CameraController.instance.ResetToPlayer();
    }

    public override Vector3 Place(Item item)
    {
        if (item.identifier.Equals(lensIdentifier) && !goalReached)
        {
            if (Mathf.RoundToInt(horizontal) == horizontalGoal && Mathf.RoundToInt(vertical) == verticalGoal)
            {
                triggeredEvent.Activate();
                goalReached = true;
            }
        }
        return base.Place(item);
    }

    public override Item Grab()
    {
        var item = base.Grab();
        if (item && item.identifier.Equals(lensIdentifier) && goalReached)
        {
            triggeredEvent.Deactivate();
            goalReached = false;
        }
        return item;
    }

    private void Update()
    {
        if (!ControlToggle.isActive)
        {
            return;
        }

        var hInput = Input.GetAxisRaw("Horizontal");
        var vInput = Input.GetAxisRaw("Vertical");

        horizontal += hInput * Time.deltaTime * sensitivity;
        vertical += vInput * Time.deltaTime * sensitivity;
        horizontal = Mathf.Clamp(horizontal, 0, 99);
        vertical = Mathf.Clamp(vertical, 0, 99);

        UpdateText(horizontalText, horizontal);
        UpdateText(verticalText, vertical);

        if (Mathf.RoundToInt(horizontal) == horizontalGoal && Mathf.RoundToInt(vertical) == verticalGoal)
        {
            if (!goalReached && heldItem != null && heldItem.identifier.Equals(lensIdentifier))
            {
                triggeredEvent.Activate();
                goalReached = true;
            }
        }
        else
        {
            if (goalReached)
            {
                triggeredEvent.Deactivate();
                goalReached = false;
            }
        }
    }

    private void UpdateText(TMP_Text textComponent, float value)
    {
        string s = Mathf.RoundToInt(value).ToString();
        if (!textComponent.text.Equals(s))
        {
            textComponent.text = s;
            textComponent.transform.DOKill();
            textComponent.transform.localScale = Vector3.one;
            textComponent.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f, 0, 0);
            tickEvent.Post(gameObject);
        }
    }

    public override string GetUseTextKey()
    {
        return "action_use";
    }

    public override bool IsUseable()
    {
        return true;
    }
}
