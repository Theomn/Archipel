using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InputHUDElement : MonoBehaviour
{
    [SerializeField] private GameObject keyboardActiveGroup, keyboardInactiveGroup;
    [SerializeField] private GameObject gamepadActiveGroup, gamepadInactiveGroup;
    [SerializeField] private TMP_Text TMPtext;

    private GameObject activeGroup, inactiveGroup;
    private InputType inputType;

    private bool isActive;
    private readonly Color grey = new Color(1, 1, 1, 0.3f);

    private void Awake()
    {
        var darkBlue = Swatches.HexToColor(Swatches.darkBlue);
        float intensity = 0.2f;
        TMPtext.outlineColor = new Color(darkBlue.r * intensity, darkBlue.g * intensity, darkBlue.b * intensity, 1);
        isActive = true;
        keyboardActiveGroup.SetActive(false);
        keyboardInactiveGroup.SetActive(false);
        gamepadActiveGroup.SetActive(false);
        gamepadInactiveGroup.SetActive(false);
        ChangeInputType((InputType)PlayerPrefs.GetInt(PauseMenu.inputType, 1));
        Deactivate();
    }

    public void Show(bool activate, string text = "")
    {
        gameObject.SetActive(true);
        if (activate) Activate();
        else Deactivate();
        TMPtext.text = text;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Activate()
    {
        if (isActive) return;

        isActive = true;
        activeGroup.SetActive(true);
        inactiveGroup.SetActive(false);
        TMPtext.color = Color.white;
        TMPtext.outlineWidth = 0.22f;
    }

    private void Deactivate()
    {
        if (!isActive) return;

        isActive = false;
        activeGroup.SetActive(false);
        inactiveGroup.SetActive(true);
        TMPtext.color = grey;
        TMPtext.outlineWidth = 0;
    }

    public void ChangeInputType(InputType inputType)
    {
        if (activeGroup) activeGroup.SetActive(false);
        if (inactiveGroup) inactiveGroup.SetActive(false);

        this.inputType = inputType;

        switch (inputType)
        {
            default:
            case InputType.Keyboard:
                activeGroup = keyboardActiveGroup;
                inactiveGroup = keyboardInactiveGroup;
                break;
            case InputType.Gamepad:
                activeGroup = gamepadActiveGroup;
                inactiveGroup = gamepadInactiveGroup;
                break;
        }

        if (isActive) activeGroup.SetActive(true);
        else inactiveGroup.SetActive(true);
    }
}
