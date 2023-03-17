using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AlienVision : SingletonMonoBehaviour<AlienVision>
{
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private RawImage background;

    private bool isActive;

    protected override void Awake()
    {
        base.Awake();
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0);
    }
    public void SetText(string key)
    {
        string text = GameController.instance.localization.GetText(key);
        string alienText = "";
        bool escapeChar = false;
        foreach (char c in text)
        {
            // tags
            if (c == '<')
            {
                escapeChar = true;
            }
            if (escapeChar)
            {
                if (c == '>')
                    escapeChar = false;
                alienText += c;
                continue;
            }

            // not tags
            if (Random.value >= 0.5f)
            {
                alienText += "<font=\"kaerukaeru-Regular SDF\">" + c + "</font>";
            }
            else
            {
                alienText += c;
            }
        }
        textComponent.text = alienText;
    }

    public void Open()
    {
        isActive = true;
        background.DOKill();
        textComponent.DOKill();
        background.DOFade(0.9f, 1f);
        textComponent.DOFade(1f, 4f);
    }

    public void Close()
    {
        isActive = false;
        background.DOKill();
        textComponent.DOKill();
        background.DOFade(0, 1f);
        textComponent.DOFade(0, 1f);
    }
}
