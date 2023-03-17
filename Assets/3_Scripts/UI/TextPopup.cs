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

    private void Start()
    {
        localization = GameController.instance.localization;
        Hide();
    }

    public void Show(string key)
    {
        textComponent.text = localization.GetText(key);
        transform.DOKill();
        transform.DOScale(Vector3.one, 0.5f);
    }

    public void FadeOut()
    {
        transform.DOKill();
        transform.DOScale(Vector3.zero, 0.5f);
    }

    public void Hide()
    {
        transform.localScale = Vector3.zero;
    }
}
