using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HUDController : SingletonMonoBehaviour<HUDController>
{
    [Header("Inputs")]
    [SerializeField] public ParticleSystem highlightParticles;
    [SerializeField] public InputHUDElement use, grab, jump, sit;
    [SerializeField] private GameObject back;


    [Header("Text Popups")]
    [SerializeField] private TextPopup defaultPopup;
    [SerializeField] private TextPopup notePopup, letterPopup, fanaticPopup, impiousStone, mixedPopup, inventionsPopup;

    [Header("Subtitle")]
    [SerializeField] private TMP_Text subtitleText;
    [SerializeField] private RawImage subtitleBackground;

    private Dictionary<TextType, TextPopup> popups;
    private Localization loc;
    private float subtitleTimer;
    protected override void Awake()
    {
        base.Awake();
        HideSubtitle();
        popups = new Dictionary<TextType, TextPopup>();
        popups.Add(TextType.Popup, defaultPopup);
        popups.Add(TextType.Note, notePopup);
        popups.Add(TextType.Letter, letterPopup);
        popups.Add(TextType.FanaticStone, fanaticPopup);
        popups.Add(TextType.ImpiousStone, impiousStone);
        popups.Add(TextType.MixedStone, mixedPopup);
        popups.Add(TextType.Inventions, inventionsPopup);
    }

    void Start()
    {
        loc = GameController.instance.localization;

        var TMPText = back.GetComponentInChildren<TMP_Text>();
        TMPText.text = loc.GetText("action_return");
        TMPText.outlineWidth = 0.22f;
        float intensity = 0.2f;
        var darkBlue = Swatches.HexToColor(Swatches.darkBlue);
        TMPText.outlineColor = new Color(darkBlue.r * intensity, darkBlue.g * intensity, darkBlue.b * intensity, 1);
        back.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DisplaySubtitle("test_subtitle", 5);
        }

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
        var newPos = position + Vector3.up * 0.25f + Vector3.forward * 0.2f;
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

    public void BackInput(bool active)
    {
        if (active)
        {
            use.Hide(); grab.Hide(); sit.Hide(); jump.Hide();
            back.SetActive(true);
        }
        else
        {
            back.SetActive(false);
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
