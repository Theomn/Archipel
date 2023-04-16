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

    [Header("Wwise")]
    [SerializeField] private AK.Wwise.Event openEvent;
    [SerializeField] private AK.Wwise.Event closeEvent;

    private float characterAppearInterval;
    private string thoughtKey;

    private DOTweenTMPAnimator charAnim;
    Sequence fadeInSequence;


    protected override void Awake()
    {
        base.Awake();
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0);
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
            if (!textComponent.textInfo.characterInfo[i].isVisible) continue;
            charAnim.DOShakeCharOffset(i, 2, 1, 15, 90, false).SetLoops(-1, LoopType.Restart);
            fadeInSequence.Append(charAnim.DOFadeChar(i, 1, characterAppearInterval).SetEase(Ease.InCirc));
        }
        fadeInSequence.Restart();
    }

    public void Open()
    {
        openEvent.Post(gameObject);
        background.DOKill();
        background.DOFade(0.9f, 1f);
        fadeInSequence.Restart();
    }

    public void Close()
    {
        closeEvent.Post(gameObject);
        background.DOKill();
        background.DOFade(0, 1f);
        fadeInSequence.Pause();
        ThoughtScreen.instance.AddThought(thoughtKey);

        for (int i = 0; i < textComponent.textInfo.characterCount; i++)
        {
            charAnim.DOFadeChar(i, 0, 0.5f);
        }
    }

    public void SetCharacterAppearInterval(float interval)
    {
        characterAppearInterval = interval;
    }

    public void SetThoughtKey(string key)
    {
        thoughtKey = key;
    }
}
