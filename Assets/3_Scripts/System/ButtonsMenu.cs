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

    public void ButtonEnter()
    {
        text.DOKill();
        text.DOFontSize(80, 0.1f);
    }

    public void ButtonExit()
    {
        text.DOKill();
        text.DOFontSize(60, 0.1f);
    }

    private void OnDisable()
    {
        text.DOKill();
        text.fontSize = 60;
    }

    public void OnDeselect(BaseEventData data)
    {
        ButtonExit();
    }


}
