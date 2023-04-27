using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonsMenu : MonoBehaviour, IDeselectHandler
{
    [SerializeField] private TextMeshProUGUI text;

    private float initialSize;

    private void Awake() {
        initialSize = text.fontSize;
    }

    public void ButtonEnter()
    {
        text.DOKill();
        text.DOFontSize(initialSize * 1.3f, 0.1f);
    }

    public void ButtonExit()
    {
        text.DOKill();
        text.DOFontSize(initialSize, 0.1f);
    }

    private void OnDisable()
    {
        text.DOKill();
        text.fontSize = initialSize;
    }

    public void OnDeselect(BaseEventData data)
    {
        ButtonExit();
    }


}
