using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HUDController : SingletonMonoBehaviour<HUDController>
{
    [Header("Inputs")]
    [SerializeField] private ParticleSystem highlightParticles;
    [SerializeField] private Transform inputRoot;
    [SerializeField] public InputHUDElement use, grab, sit, back, diary;
    [SerializeField] private GameObject backk;


    [Header("Text Popups")]
    [SerializeField] private TextPopup defaultPopup;
    [SerializeField] private TextPopup notePopup, letterPopup, fanaticPopup, impiousStone, mixedPopup, inventionsPopup, FanLetterPopup, ImpLetterPopup, FanNotePopup, ImpNotePopup;

    [Header("Subtitle")]
    [SerializeField] private TMP_Text subtitleText;
    [SerializeField] private RawImage subtitleBackground;

    [Header("Other")]
    [SerializeField] private Image blackScreen;
    [SerializeField] private TMP_Text loadingText;

    private Dictionary<TextType, TextPopup> popups;
    private Localization loc;
    private float subtitleTimer;
    protected override void Awake()
    {
        base.Awake();
        HideSubtitle();
        InitializePopups();
        Blackout(false);
    }

    private void InitializePopups()
    {
        popups = new Dictionary<TextType, TextPopup>();
        popups.Add(TextType.Popup, defaultPopup);
        popups.Add(TextType.Note, notePopup);
        popups.Add(TextType.Letter, letterPopup);
        popups.Add(TextType.FanaticStone, fanaticPopup);
        popups.Add(TextType.ImpiousStone, impiousStone);
        popups.Add(TextType.MixedStone, mixedPopup);
        popups.Add(TextType.Inventions, inventionsPopup);
        popups.Add(TextType.FanaticLetter, FanLetterPopup);
        popups.Add(TextType.ImpiousLetter, ImpLetterPopup);
        popups.Add(TextType.FanaticNote, FanNotePopup);
        popups.Add(TextType.ImpiousNote, ImpNotePopup);
    }

    void Start()
    {
        loc = GameController.instance.localization;
        ChangeInputType((InputType)PlayerPrefs.GetInt(PauseMenu.inputType, 0));
        back.Hide();
    }

    void Update()
    {
        if (subtitleTimer > 0)
        {
            subtitleTimer -= Time.deltaTime;
            if (subtitleTimer <= 0)
            {
                FadeOutSubtitle();
            }
        }
    }

    public void ShowHighlightParticles(Vector3 position)
    {
        var newPos = position + Vector3.up * 0.20f + Vector3.forward * 0.19f;
        highlightParticles.transform.position = newPos;
        if (!highlightParticles.isEmitting)
        {
            highlightParticles.Play();
        }
    }

    public void HideHighlightParticles()
    {
        highlightParticles.Stop();
    }

    public void ShowInputs(bool active)
    {
        if (active)
        {
            inputRoot.gameObject.SetActive(true);
        }
        else
        {
            inputRoot.gameObject.SetActive(false);
        }
    }

    public void ChangeInputType(InputType inputType)
    {
        use.ChangeInputType(inputType);
        grab.ChangeInputType(inputType);
        sit.ChangeInputType(inputType);
        diary.ChangeInputType(inputType);
        back.ChangeInputType(inputType);
    }


    public void BackInput(bool active)
    {
        if (active)
        {
            use.Hide(); grab.Hide(); sit.Hide(); diary.Hide();
            back.Show(true, loc.GetText("action_return"));
        }
        else
        {
            back.Hide();
        }
    }

    public void DisplayText(TextType textType, string key)
    {
        popups[textType].Show(key);
    }

    public void CloseText(TextType textType)
    {
        popups[textType].FadeOut();
    }

    /// <summary>
    ///  Displays a subtitle for duration seconds.
    /// </summary>
    public void DisplaySubtitle(string key, float duration)
    {
        subtitleText.text = loc.GetText(key);
        subtitleText.DOKill();
        subtitleText.DOFade(1, 1);
        subtitleBackground.DOKill();
        subtitleBackground.DOFade(0.8f, 1);
        subtitleTimer = duration;
    }

    public void Blackout(bool activate)
    {
        Blackout(activate, 0.3f, null);
    }

    public void Blackout(bool activate, Action callback)
    {
        Blackout(activate, 0.3f, callback);
    }

    public void Blackout(bool activate, float duration, Action callback = null)
    {
        if (activate)
        {
            blackScreen.DOKill();
            if (callback != null) blackScreen.DOFade(1, duration).SetEase(Ease.OutCubic).onComplete += () => callback();
            else blackScreen.DOFade(1, duration).SetEase(Ease.OutCubic);
        }
        else
        {
            blackScreen.DOKill();
            if (callback != null) blackScreen.DOFade(0, duration).SetEase(Ease.InCubic).onComplete += () => callback();
            else blackScreen.DOFade(0, duration).SetEase(Ease.InCubic);
        }
    }

    public void BlackoutInstant()
    {
        blackScreen.DOKill();
        blackScreen.color = Utils.ChangeColorAlpha(blackScreen.color, 1);
    }

    public void SetLoadingText(string text)
    {
        loadingText.text = text;
    }

    private void FadeOutSubtitle()
    {
        subtitleText.DOKill();
        subtitleText.DOFade(0, 1);
        subtitleBackground.DOKill();
        subtitleBackground.DOFade(0, 1);
    }

    private void HideSubtitle()
    {
        subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 0);
        subtitleBackground.color = new Color(subtitleBackground.color.r, subtitleBackground.color.g, subtitleBackground.color.b, 0);
    }
}
