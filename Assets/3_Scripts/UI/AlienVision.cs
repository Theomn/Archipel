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

    private TMP_TextInfo textInfo;

    private DOTweenTMPAnimator charAnim;
    Sequence fadeInSequence = DOTween.Sequence();


    private bool isActive;

    protected override void Awake()
    {
        base.Awake();
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0);
        textInfo = textComponent.textInfo;
        charAnim = new DOTweenTMPAnimator(textComponent);

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
        textComponent.ForceMeshUpdate();
        fadeInSequence.Kill();
        fadeInSequence = DOTween.Sequence().Pause().SetAutoKill(false);
        for (int i = 0; i < textComponent.textInfo.characterCount; i++)
        {
            charAnim.DOShakeCharOffset(i, 2, 1, 15, 90, false).SetLoops(-1, LoopType.Restart);
            fadeInSequence.Append(charAnim.DOFadeChar(i, 1, 0.2f).SetEase(Ease.InCirc));
        }
        fadeInSequence.Restart();
    }

    public void Open()
    {
        isActive = true;
        background.DOKill();
        background.DOFade(0.9f, 1f);
        fadeInSequence.Restart();
    }

    public void Close()
    {
        isActive = false;
        background.DOKill();
        background.DOFade(0, 1f);
        fadeInSequence.Pause();

        for (int i = 0; i < textComponent.textInfo.characterCount; i++)
        {
            charAnim.DOFadeChar(i, 0, 1f);
        }
    }
}
