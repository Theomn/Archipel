using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InputHUDElement : MonoBehaviour
{
    [SerializeField] private Image buttonImage, backgroundImage;
    [SerializeField] private Color buttonColor;
    [SerializeField] private TMP_Text TMPtext;

    private bool isActive = true;

    private readonly Color grey = new Color(1, 1, 1, 0.3f);

    private void Awake()
    {
        var darkBlue = Swatches.HexToColor(Swatches.darkBlue);
        float intensity = 0.2f;
        TMPtext.outlineColor = new Color(darkBlue.r * intensity, darkBlue.g * intensity, darkBlue.b * intensity, 1);
        backgroundImage.color = darkBlue;
    }

    public void Show(bool activate, string text = "")
    {
        if (activate) Activate();
        else Deactivate();
        TMPtext.text = text;
    }

    private void Activate()
    {
        if (isActive) return;

        isActive = true;
        buttonImage.color = buttonColor;
        backgroundImage.enabled = true;
        TMPtext.color = Color.white;
        TMPtext.outlineWidth = 0.22f;
        buttonImage.transform.DORestart();
        buttonImage.transform.DOKill();
        //image.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 0, 0);
    }

    private void Deactivate()
    {
        if (!isActive) return;

        isActive = false;
        buttonImage.color = grey;
        backgroundImage.enabled = false;
        TMPtext.color = grey;
        TMPtext.outlineWidth = 0;
    }
}
