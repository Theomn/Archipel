using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public enum TextType
{
    Popup
}

public class TextPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text textComponent;
    private Localization localization;
    private PlayerController player;
    private bool isActive;
    private bool activateFlag;

    private void Start()
    {
        localization = GameController.instance.localization;
        Hide();
    }

    private void Update()
    {
        if (activateFlag)
        {
            // prevent unpausing the frame it opens
            isActive = true;
            activateFlag = false;
            return;
        }

        if (isActive)
        {
            if (Input.GetButtonDown("Use") || Input.GetButtonDown("Grab") || Input.GetButtonDown("Jump"))
            {
                FadeOut();
            }
        }
    }

    public void Show(string key)
    {
        PlayerController.instance.Pause(true);
        PlayerItem.instance.Pause(true);
        activateFlag = true;
        textComponent.text = localization.GetText(key);
        transform.DOKill();
        transform.DOScale(Vector3.one, 0.5f);
    }

    public void FadeOut()
    {
        isActive = false;
        PlayerController.instance.Pause(false);
        PlayerItem.instance.Pause(false);
        transform.DOKill();
        transform.DOScale(Vector3.zero, 0.5f);
    }

    public void Hide()
    {
        isActive = false;
        PlayerController.instance.Pause(false);
        PlayerItem.instance.Pause(false);
        transform.localScale = Vector3.zero;
    }
}
