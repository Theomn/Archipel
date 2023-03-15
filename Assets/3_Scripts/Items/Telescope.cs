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
    private float horizontal = 36, vertical = 61;

    private bool isActive;
    private bool activateFlag;
    private bool goalReached;

    private void Start()
    {
        UpdateText(horizontalText, horizontal);
        UpdateText(verticalText, vertical);
    }

    public void Use()
    {
        activateFlag = true;
        PlayerController.instance.Pause(true);
        PlayerItem.instance.Pause(true);
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
        if (item.identifier.Equals(lensIdentifier) && goalReached)
        {
            triggeredEvent.Deactivate();
            goalReached = false;
        }
        return item;
    }

    private void Update()
    {
        if (!isActive)
        {
            if (activateFlag)
            {
                // prevent unpausing the frame it opens
                isActive = true;
                activateFlag = false;
            }
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

        if (Input.GetButtonDown("Use") || Input.GetButtonDown("Grab") || Input.GetButtonDown("Jump"))
        {
            isActive = false;
            PlayerController.instance.Pause(false);
            PlayerItem.instance.Pause(false);
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
        }
    }
}
